using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Infrastructure.Persistence;
using UtilityPayments.Infrastructure.UtilityPaymentAgreements;

namespace UtilityPayments.Infrastructure.Tests.UtilityPaymentAgreements;

public class UtilityPaymentAgreementRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=utility_payments_tests;Username=utility_user;Password=utility_password";

    [Fact]
    public async Task GetActiveAsync_ShouldReturnOnlyActiveAgreements_OrderedByName()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UtilityPaymentsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var dbContext = new UtilityPaymentsDbContext(options);

        await CleanTableAsync(dbContext);

        try
        {
            dbContext.UtilityPaymentAgreements.AddRange(
                new UtilityPaymentAgreement
                {
                    Code = "WATER",
                    Name = "Water",
                    Description = "Water utility payment",
                    IntegrationKey = "WATER_MOCK",
                    AllowInformationRequest = true,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                },
                new UtilityPaymentAgreement
                {
                    Code = "ELECTRICITY",
                    Name = "Electricity",
                    Description = "Electricity utility payment",
                    IntegrationKey = "ELECTRICITY_MOCK",
                    AllowInformationRequest = true,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                },
                new UtilityPaymentAgreement
                {
                    Code = "INACTIVE",
                    Name = "Inactive Service",
                    Description = "Inactive utility payment",
                    IntegrationKey = "INACTIVE_MOCK",
                    AllowInformationRequest = true,
                    IsActive = false,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                });

            await dbContext.SaveChangesAsync();

            var repository = new UtilityPaymentAgreementRepository(dbContext);

            // Act
            var result = await repository.GetActiveAsync();

            // Assert
            Assert.Collection(
                result,
                first =>
                {
                    Assert.Equal("ELECTRICITY", first.Code);
                    Assert.True(first.IsActive);
                },
                second =>
                {
                    Assert.Equal("WATER", second.Code);
                    Assert.True(second.IsActive);
                });
        }
        finally
        {
            await CleanTableAsync(dbContext);
        }
    }
    private static async Task CleanTableAsync(
        UtilityPaymentsDbContext dbContext)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            "TRUNCATE TABLE public.utility_payment_agreements RESTART IDENTITY;");
    }
}
