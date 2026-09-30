using UtilityPayments.Application.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentAgreements;

namespace UtilityPayments.Application.Tests.UtilityPaymentAgreements;

public class GetActiveUtilityPaymentAgreementsTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedAgreements()
    {
        // Arrange
        var agreements = new List<UtilityPaymentAgreement>
        {
            new()
            {
                Id = 1,
                Code = "ELECTRICITY",
                Name = "Electricity",
                Description = "Electricity utility payment",
                IntegrationKey = "ELECTRICITY_MOCK",
                IsActive = true
            },
            new()
            {
                Id = 2,
                Code = "WATER",
                Name = "Water",
                Description = "Water utility payment",
                IntegrationKey = "WATER_MOCK",
                IsActive = true
            }
        };

        var repository = new FakeUtilityPaymentAgreementRepository(agreements);

        var useCase = new GetActiveUtilityPaymentAgreements(repository);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.Collection(
            result,
            first =>
            {
                Assert.Equal(1, first.Id);
                Assert.Equal("ELECTRICITY", first.Code);
                Assert.Equal("Electricity", first.Name);
                Assert.Equal("Electricity utility payment", first.Description);
            },
            second =>
            {
                Assert.Equal(2, second.Id);
                Assert.Equal("WATER", second.Code);
                Assert.Equal("Water", second.Name);
                Assert.Equal("Water utility payment", second.Description);
            });
    }

    private sealed class FakeUtilityPaymentAgreementRepository(
        IReadOnlyCollection<UtilityPaymentAgreement> agreements)
        : IUtilityPaymentAgreementRepository
    {
        public Task<IReadOnlyCollection<UtilityPaymentAgreement>> GetActiveAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(agreements);
        }
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyCollection_WhenNoAgreementsExist()
    {
        // Arrange
        var repository = new FakeUtilityPaymentAgreementRepository(
            Array.Empty<UtilityPaymentAgreement>());

        var useCase = new GetActiveUtilityPaymentAgreements(repository);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.Empty(result);
    }
}
