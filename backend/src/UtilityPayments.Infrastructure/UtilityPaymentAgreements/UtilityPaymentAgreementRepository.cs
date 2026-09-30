using Microsoft.EntityFrameworkCore;
using UtilityPayments.Application.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Infrastructure.UtilityPaymentAgreements;

public class UtilityPaymentAgreementRepository(
    UtilityPaymentsDbContext dbContext)
    : IUtilityPaymentAgreementRepository
{
    public async Task<IReadOnlyCollection<UtilityPaymentAgreement>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.UtilityPaymentAgreements
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
