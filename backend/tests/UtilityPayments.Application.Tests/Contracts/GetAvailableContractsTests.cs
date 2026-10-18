using UtilityPayments.Application.Contracts;

namespace UtilityPayments.Application.Tests.Contracts;

public class GetAvailableContractsTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedContracts()
    {
        // Arrange
        var contracts = new List<AvailableContract>
        {
            new(
                1,
                "001234567890",
                "Savings Plus",
                "SAVINGS_ACCOUNT",
                "USD"),
            new(
                2,
                "009876543210",
                "Current Standard",
                "CURRENT_ACCOUNT",
                "USD")
        };

        var repository = new FakeContractRepository(contracts);

        var useCase = new GetAvailableContracts(repository);

        // Act
        var result = await useCase.ExecuteAsync(1);

        // Assert
        Assert.Collection(
            result,
            first =>
            {
                Assert.Equal(1, first.Id);
                Assert.Equal("001234567890", first.ContractNumber);
                Assert.Equal("Savings Plus", first.ProductName);
                Assert.Equal("SAVINGS_ACCOUNT", first.ContractType);
                Assert.Equal("USD", first.Currency);
            },
            second =>
            {
                Assert.Equal(2, second.Id);
                Assert.Equal("009876543210", second.ContractNumber);
                Assert.Equal("Current Standard", second.ProductName);
                Assert.Equal("CURRENT_ACCOUNT", second.ContractType);
                Assert.Equal("USD", second.Currency);
            });
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyCollection_WhenNoContractsExist()
    {
        // Arrange
        var repository = new FakeContractRepository(
            Array.Empty<AvailableContract>());

        var useCase = new GetAvailableContracts(repository);

        // Act
        var result = await useCase.ExecuteAsync(1);

        // Assert
        Assert.Empty(result);
    }

    private sealed class FakeContractRepository(
        IReadOnlyCollection<AvailableContract> contracts)
        : IContractRepository
    {
        public Task<IReadOnlyCollection<AvailableContract>> GetAvailableByClientIdAsync(
            long clientId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(contracts);
        }
    }
}
