using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UtilityPayments.Application.UtilityPaymentRequiredInfos;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Api.Tests.UtilityPaymentRequiredInfos;

[Collection("Database")]
public class GetUtilityPaymentRequiredInfosTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public GetUtilityPaymentRequiredInfosTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnRequiredInfosForAgreement()
    {
        // Arrange
        await CleanDatabaseAsync();

        try
        {
            using var scope = _factory.Services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<UtilityPaymentsDbContext>();

            var agreement = new UtilityPaymentAgreement
            {
                Code = "WATER",
                Name = "Water",
                Description = "Water utility payment",
                IntegrationKey = "WATER_MOCK",
                AllowInformationRequest = true,
                IsActive = true,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            dbContext.UtilityPaymentAgreements.Add(agreement);

            await dbContext.SaveChangesAsync();

            dbContext.UtilityPaymentRequiredInfos.AddRange(
                new UtilityPaymentRequiredInfo
                {
                    UtilityPaymentAgreementId = agreement.Id,
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
                    UtilityPaymentAgreementId = agreement.Id,
                    Code = "ACCOUNT_NUMBER",
                    Label = "Account Number",
                    DataType = RequiredInfoDataType.Text,
                    IsRequired = true,
                    DisplayOrder = 1,
                    MaxLength = 20,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                });

            await dbContext.SaveChangesAsync();

            // Act
            var response = await _client.GetAsync(
                $"/api/utility-payment-agreements/{agreement.Id}/required-info");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var requiredInfos =
                await response.Content.ReadFromJsonAsync<
                    List<UtilityPaymentRequiredInfoResponse>>();

            Assert.NotNull(requiredInfos);

            Assert.Collection(
                requiredInfos,
                first =>
                {
                    Assert.Equal("ACCOUNT_NUMBER", first.Code);
                    Assert.Equal(1, first.DisplayOrder);
                },
                second =>
                {
                    Assert.Equal("METER_NUMBER", second.Code);
                    Assert.Equal(2, second.DisplayOrder);
                });
        }
        finally
        {
            await CleanDatabaseAsync();
        }
    }

    private async Task CleanDatabaseAsync()
    {
        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<UtilityPaymentsDbContext>();

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            TRUNCATE TABLE
                public.utility_payment_required_info,
                public.utility_payment_agreements
            RESTART IDENTITY;
            """);
    }
}
