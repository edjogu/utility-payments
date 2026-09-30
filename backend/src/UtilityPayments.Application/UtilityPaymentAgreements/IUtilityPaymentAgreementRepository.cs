using UtilityPayments.Domain.UtilityPaymentAgreements;

namespace UtilityPayments.Application.UtilityPaymentAgreements;

public interface IUtilityPaymentAgreementRepository
{
    Task<IReadOnlyCollection<UtilityPaymentAgreement>> GetActiveAsync(
        CancellationToken cancellationToken = default);
}
