using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.Clients;
using UtilityPayments.Domain.Contracts;
using UtilityPayments.Domain.Currencies;
using UtilityPayments.Infrastructure.Contracts;
using UtilityPayments.Infrastructure.Persistence;
using UtilityPayments.Infrastructure.Tests.TestInfrastructure;

namespace UtilityPayments.Infrastructure.Tests.Contracts;

[Collection("Database")]
public class ContractRepositoryTests
{
    [Fact]
    public async Task GetAvailableByClientIdAsync_ShouldReturnOnlyEligibleContracts()
    {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();

        await CleanTablesAsync(dbContext);

        try
        {
            var clientType = new ClientType
            {
                Code = "NATURAL_PERSON",
                Name = "Natural Person",
                IsActive = true
            };

            var savingsAccountType = new ContractType
            {
                Code = "SAVINGS_ACCOUNT",
                Name = "Savings Account",
                IsActive = true
            };

            var currentAccountType = new ContractType
            {
                Code = "CURRENT_ACCOUNT",
                Name = "Current Account",
                IsActive = true
            };

            var creditCardType = new ContractType
            {
                Code = "CREDIT_CARD",
                Name = "Credit Card",
                IsActive = true
            };

            var activeStatus = new ContractStatus
            {
                Code = "ACTIVE",
                Name = "Active",
                IsActive = true
            };

            var closedStatus = new ContractStatus
            {
                Code = "CLOSED",
                Name = "Closed",
                IsActive = true
            };

            var ownerRelationType = new CustomerContractRelationType
            {
                Code = "OWNER",
                Name = "Owner",
                IsActive = true
            };

            var authorizedRelationType = new CustomerContractRelationType
            {
                Code = "AUTHORIZED",
                Name = "Authorized",
                IsActive = true
            };

            var currency = new Currency
            {
                Code = "USD",
                Name = "US Dollar",
                IsActive = true
            };

            dbContext.ClientTypes.Add(clientType);

            dbContext.ContractTypes.AddRange(
                savingsAccountType,
                currentAccountType,
                creditCardType);

            dbContext.ContractStatuses.AddRange(
                activeStatus,
                closedStatus);

            dbContext.CustomerContractRelationTypes.AddRange(
                ownerRelationType,
                authorizedRelationType);

            dbContext.Currencies.Add(currency);

            await dbContext.SaveChangesAsync();

            var client = new Client
            {
                ClientTypeId = clientType.Id,
                ClientCode = "CL000001",
                DocumentNumber = "123456789",
                FirstName = "Test",
                LastName = "Customer",
                IsActive = true
            };

            dbContext.Clients.Add(client);

            var savingsProduct = new ContractProduct
            {
                ContractTypeId = savingsAccountType.Id,
                CurrencyId = currency.Id,
                Code = "SAVINGS_PLUS",
                Name = "Savings Plus",
                IsActive = true
            };

            var currentProduct = new ContractProduct
            {
                ContractTypeId = currentAccountType.Id,
                CurrencyId = currency.Id,
                Code = "CURRENT_STANDARD",
                Name = "Current Standard",
                IsActive = true
            };

            var creditCardProduct = new ContractProduct
            {
                ContractTypeId = creditCardType.Id,
                CurrencyId = currency.Id,
                Code = "CREDIT_GOLD",
                Name = "Credit Gold",
                IsActive = true
            };

            dbContext.ContractProducts.AddRange(
                savingsProduct,
                currentProduct,
                creditCardProduct);

            await dbContext.SaveChangesAsync();

            var savingsContract = new Contract
            {
                ContractNumber = "001234567890",
                ContractProductId = savingsProduct.Id,
                ContractStatusId = activeStatus.Id,
                ContractDate = new DateOnly(2025, 3, 15)
            };

            var currentContract = new Contract
            {
                ContractNumber = "009876543210",
                ContractProductId = currentProduct.Id,
                ContractStatusId = activeStatus.Id,
                ContractDate = new DateOnly(2024, 8, 1)
            };

            var creditCardContract = new Contract
            {
                ContractNumber = "005555555555",
                ContractProductId = creditCardProduct.Id,
                ContractStatusId = activeStatus.Id,
                ContractDate = new DateOnly(2026, 1, 10)
            };

            var closedContract = new Contract
            {
                ContractNumber = "001111111111",
                ContractProductId = savingsProduct.Id,
                ContractStatusId = closedStatus.Id,
                ContractDate = new DateOnly(2023, 2, 20)
            };

            var authorizedContract = new Contract
            {
                ContractNumber = "007777777777",
                ContractProductId = savingsProduct.Id,
                ContractStatusId = activeStatus.Id,
                ContractDate = new DateOnly(2025, 6, 10)
            };

            dbContext.Contracts.AddRange(
                savingsContract,
                currentContract,
                creditCardContract,
                closedContract,
                authorizedContract);

            await dbContext.SaveChangesAsync();

            dbContext.CustomerContractRelations.AddRange(
                new CustomerContractRelation
                {
                    ClientId = client.Id,
                    ContractId = savingsContract.Id,
                    CustomerContractRelationTypeId = ownerRelationType.Id,
                    IsActive = true
                },
                new CustomerContractRelation
                {
                    ClientId = client.Id,
                    ContractId = currentContract.Id,
                    CustomerContractRelationTypeId = ownerRelationType.Id,
                    IsActive = true
                },
                new CustomerContractRelation
                {
                    ClientId = client.Id,
                    ContractId = creditCardContract.Id,
                    CustomerContractRelationTypeId = ownerRelationType.Id,
                    IsActive = true
                },
                new CustomerContractRelation
                {
                    ClientId = client.Id,
                    ContractId = closedContract.Id,
                    CustomerContractRelationTypeId = ownerRelationType.Id,
                    IsActive = true
                },
                new CustomerContractRelation
                {
                    ClientId = client.Id,
                    ContractId = authorizedContract.Id,
                    CustomerContractRelationTypeId = authorizedRelationType.Id,
                    IsActive = true
                });

            await dbContext.SaveChangesAsync();

            var repository = new ContractRepository(dbContext);

            // Act
            var result = await repository.GetAvailableByClientIdAsync(
                client.Id);

            // Assert
            Assert.Collection(
                result,
                first =>
                {
                    Assert.Equal("001234567890", first.ContractNumber);
                    Assert.Equal("Savings Plus", first.ProductName);
                    Assert.Equal("SAVINGS_ACCOUNT", first.ContractType);
                    Assert.Equal("USD", first.Currency);
                },
                second =>
                {
                    Assert.Equal("009876543210", second.ContractNumber);
                    Assert.Equal("Current Standard", second.ProductName);
                    Assert.Equal("CURRENT_ACCOUNT", second.ContractType);
                    Assert.Equal("USD", second.Currency);
                });
        }
        finally
        {
            await CleanTablesAsync(dbContext);
        }
    }

    private static async Task CleanTablesAsync(
        UtilityPaymentsDbContext dbContext)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            """
            TRUNCATE TABLE
                public.customer_contract_relations,
                public.contracts,
                public.contract_products,
                public.clients,
                public.cls_customer_contract_relation_types,
                public.cls_contract_statuses,
                public.cls_contract_types,
                public.cls_currencies,
                public.cls_client_types
            RESTART IDENTITY;
            """);
    }
}
