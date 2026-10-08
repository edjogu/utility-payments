using UtilityPayments.Application.UtilityPaymentRequiredInfos;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Application.Tests.UtilityPaymentRequiredInfos;

public class GetActiveUtilityPaymentRequiredInfosTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedRequiredInfos()
    {
        // Arrange
        var requiredInfos = new List<UtilityPaymentRequiredInfo>
        {
            new()
            {
                Id = 1,
                UtilityPaymentAgreementId = 2,
                Code = "ACCOUNT_NUMBER",
                Label = "Account Number",
                DataType = RequiredInfoDataType.Text,
                IsRequired = true,
                DisplayOrder = 1,
                MaxLength = 20,
                IsActive = true
            },
            new()
            {
                Id = 2,
                UtilityPaymentAgreementId = 2,
                Code = "METER_NUMBER",
                Label = "Meter Number",
                DataType = RequiredInfoDataType.Text,
                IsRequired = true,
                DisplayOrder = 2,
                MaxLength = 20,
                IsActive = true
            }
        };

        var repository =
            new FakeUtilityPaymentRequiredInfoRepository(requiredInfos);

        var useCase =
            new GetActiveUtilityPaymentRequiredInfos(repository);

        // Act
        var result = await useCase.ExecuteAsync(2);

        // Assert
        Assert.Collection(
            result,
            first =>
            {
                Assert.Equal(1, first.Id);
                Assert.Equal("ACCOUNT_NUMBER", first.Code);
                Assert.Equal("Account Number", first.Label);
                Assert.Equal("text", first.DataType);
                Assert.True(first.IsRequired);
                Assert.Equal(1, first.DisplayOrder);
                Assert.Equal(20, first.MaxLength);
            },
            second =>
            {
                Assert.Equal(2, second.Id);
                Assert.Equal("METER_NUMBER", second.Code);
                Assert.Equal("Meter Number", second.Label);
                Assert.Equal("text", second.DataType);
                Assert.True(second.IsRequired);
                Assert.Equal(2, second.DisplayOrder);
                Assert.Equal(20, second.MaxLength);
            });
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyCollection_WhenNoRequiredInfosExist()
    {
        // Arrange
        var repository =
            new FakeUtilityPaymentRequiredInfoRepository(
                Array.Empty<UtilityPaymentRequiredInfo>());

        var useCase =
            new GetActiveUtilityPaymentRequiredInfos(repository);

        // Act
        var result = await useCase.ExecuteAsync(2);

        // Assert
        Assert.Empty(result);
    }

    private sealed class FakeUtilityPaymentRequiredInfoRepository(
        IReadOnlyCollection<UtilityPaymentRequiredInfo> requiredInfos)
        : IUtilityPaymentRequiredInfoRepository
    {
        public Task<IReadOnlyCollection<UtilityPaymentRequiredInfo>>
            GetActiveByAgreementIdAsync(
                long utilityPaymentAgreementId,
                CancellationToken cancellationToken = default)
        {
            return Task.FromResult(requiredInfos);
        }
    }
}
