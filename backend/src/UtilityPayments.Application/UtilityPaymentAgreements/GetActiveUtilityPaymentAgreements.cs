namespace UtilityPayments.Application.UtilityPaymentAgreements;

public class GetActiveUtilityPaymentAgreements(
    IUtilityPaymentAgreementRepository repository)
{
    public async Task<IReadOnlyCollection<UtilityPaymentAgreementResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var agreements = await repository.GetActiveAsync(cancellationToken);

        return agreements
            .Select(x => new UtilityPaymentAgreementResponse(
                x.Id,
                x.Code,
                x.Name,
                x.Description))
            .ToList();
    }
}
