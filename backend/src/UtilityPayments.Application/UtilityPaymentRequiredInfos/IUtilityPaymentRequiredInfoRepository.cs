using UtilityPayments.Domain.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Application.UtilityPaymentRequiredInfos;

public interface IUtilityPaymentRequiredInfoRepository
{
    Task<IReadOnlyCollection<UtilityPaymentRequiredInfo>> GetActiveByAgreementIdAsync(
        long utilityPaymentAgreementId,
        CancellationToken cancellationToken = default);
}
