using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UtilityPayments.Application.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Api.Tests.UtilityPaymentAgreements;

public class GetUtilityPaymentAgreementsTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public GetUtilityPaymentAgreementsTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnOkWithEmptyCollection_WhenNoAgreementsExist()
    {
        // Arrange
        await CleanDatabaseAsync();

        try
        {
            // Act
            var response = await _client.GetAsync(
                "/api/utility-payment-agreements");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var agreements =
                await response.Content.ReadFromJsonAsync<
                    List<UtilityPaymentAgreementResponse>>();

            Assert.NotNull(agreements);
            Assert.Empty(agreements);
        }
        finally
        {
            await CleanDatabaseAsync();
        }
    }

    [Fact]
    public async Task Get_ShouldReturnActiveAgreements()
    {
        // Arrange
        await CleanDatabaseAsync();

        try
        {
            using var scope = _factory.Services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<UtilityPaymentsDbContext>();

            dbContext.UtilityPaymentAgreements.Add(
                new UtilityPaymentAgreement
                {
                    Code = "ELECTRICITY",
                    Name = "Electricity",
                    Description = "Electricity utility payment",
                    IntegrationKey = "ELECTRICITY_MOCK",
                    AllowInformationRequest = true,
                    IsActive = true,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                });

            await dbContext.SaveChangesAsync();

            // Act
            var response = await _client.GetAsync(
                "/api/utility-payment-agreements");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var agreements =
                await response.Content.ReadFromJsonAsync<
                    List<UtilityPaymentAgreementResponse>>();

            Assert.NotNull(agreements);

            var agreement = Assert.Single(agreements);

            Assert.Equal(1, agreement.Id);
            Assert.Equal("ELECTRICITY", agreement.Code);
            Assert.Equal("Electricity", agreement.Name);
            Assert.Equal(
                "Electricity utility payment",
                agreement.Description);
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
            "TRUNCATE TABLE public.utility_payment_agreements RESTART IDENTITY;");
    }
}
