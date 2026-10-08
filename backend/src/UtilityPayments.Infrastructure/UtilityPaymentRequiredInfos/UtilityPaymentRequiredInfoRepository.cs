using Microsoft.EntityFrameworkCore;
using UtilityPayments.Application.UtilityPaymentRequiredInfos;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Infrastructure.UtilityPaymentRequiredInfos;

public class UtilityPaymentRequiredInfoRepository(
    UtilityPaymentsDbContext dbContext)
    : IUtilityPaymentRequiredInfoRepository
{
    public async Task<IReadOnlyCollection<UtilityPaymentRequiredInfo>> GetActiveByAgreementIdAsync(
        long utilityPaymentAgreementId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.UtilityPaymentRequiredInfos
            .AsNoTracking()
            .Where(x =>
                x.UtilityPaymentAgreementId == utilityPaymentAgreementId &&
                x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}
