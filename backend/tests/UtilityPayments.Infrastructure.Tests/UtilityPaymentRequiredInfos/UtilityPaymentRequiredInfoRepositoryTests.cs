using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;
using UtilityPayments.Infrastructure.Persistence;
using UtilityPayments.Infrastructure.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Infrastructure.Tests.UtilityPaymentRequiredInfos;

[Collection("Database")]
public class UtilityPaymentRequiredInfoRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=utility_payments_tests;Username=utility_user;Password=utility_password";

    [Fact]
    public async Task GetActiveByAgreementIdAsync_ShouldReturnOnlyActiveRequiredInfos_OrderedByDisplayOrder()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UtilityPaymentsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var dbContext = new UtilityPaymentsDbContext(options);

        await CleanTablesAsync(dbContext);

        try
        {
            var waterAgreement = new UtilityPaymentAgreement
            {
                Code = "WATER",
                Name = "Water",
                Description = "Water utility payment",
                IntegrationKey = "WATER_MOCK",
                AllowInformationRequest = true,
                IsActive = true,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            var electricityAgreement = new UtilityPaymentAgreement
            {
                Code = "ELECTRICITY",
                Name = "Electricity",
                Description = "Electricity utility payment",
                IntegrationKey = "ELECTRICITY_MOCK",
                AllowInformationRequest = true,
                IsActive = true,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            dbContext.UtilityPaymentAgreements.AddRange(
                waterAgreement,
                electricityAgreement);

            await dbContext.SaveChangesAsync();

            dbContext.UtilityPaymentRequiredInfos.AddRange(
                new UtilityPaymentRequiredInfo
                {
                    UtilityPaymentAgreementId = waterAgreement.Id,
                    Code = "METER_NUMBER",
                    Label = "Meter Number",
                    DataType = RequiredInfoDataType.Text,
                    IsRequired = true,
                    DisplayOrder = 2,
                    MaxLength = 20,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                },
                new UtilityPaymentRequiredInfo
                {
                    UtilityPaymentAgreementId = waterAgreement.Id,
                    Code = "ACCOUNT_NUMBER",
                    Label = "Account Number",
                    DataType = RequiredInfoDataType.Text,
                    IsRequired = true,
                    DisplayOrder = 1,
                    MaxLength = 20,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                },
                new UtilityPaymentRequiredInfo
                {
                    UtilityPaymentAgreementId = waterAgreement.Id,
                    Code = "INACTIVE_FIELD",
                    Label = "Inactive Field",
                    DataType = RequiredInfoDataType.Text,
                    IsRequired = true,
                    DisplayOrder = 3,
                    IsActive = false,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                },
                new UtilityPaymentRequiredInfo
                {
                    UtilityPaymentAgreementId = electricityAgreement.Id,
                    Code = "CONTRACT_NUMBER",
                    Label = "Contract Number",
                    DataType = RequiredInfoDataType.Text,
                    IsRequired = true,
                    DisplayOrder = 1,
                    MaxLength = 20,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                });

            await dbContext.SaveChangesAsync();

            var repository =
                new UtilityPaymentRequiredInfoRepository(dbContext);

            // Act
            var result = await repository.GetActiveByAgreementIdAsync(
                waterAgreement.Id);

            // Assert
            Assert.Collection(
                result,
                first =>
                {
                    Assert.Equal("ACCOUNT_NUMBER", first.Code);
                    Assert.Equal(1, first.DisplayOrder);
                    Assert.True(first.IsActive);
                },
                second =>
                {
                    Assert.Equal("METER_NUMBER", second.Code);
                    Assert.Equal(2, second.DisplayOrder);
                    Assert.True(second.IsActive);
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
                public.utility_payment_required_info,
                public.utility_payment_agreements
            RESTART IDENTITY;
            """);
    }
}
