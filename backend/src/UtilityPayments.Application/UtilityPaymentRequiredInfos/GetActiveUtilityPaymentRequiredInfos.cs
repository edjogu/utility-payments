namespace UtilityPayments.Application.UtilityPaymentRequiredInfos;

public class GetActiveUtilityPaymentRequiredInfos(
    IUtilityPaymentRequiredInfoRepository repository)
{
    public async Task<IReadOnlyCollection<UtilityPaymentRequiredInfoResponse>> ExecuteAsync(
        long utilityPaymentAgreementId,
        CancellationToken cancellationToken = default)
    {
        var requiredInfos = await repository.GetActiveByAgreementIdAsync(
            utilityPaymentAgreementId,
            cancellationToken);

        return requiredInfos
            .Select(x => new UtilityPaymentRequiredInfoResponse(
                x.Id,
                x.Code,
                x.Label,
                x.DataType.ToString().ToLowerInvariant(),
                x.IsRequired,
                x.DisplayOrder,
                x.MinLength,
                x.MaxLength,
                x.ValidationPattern,
                x.Placeholder))
            .ToList();
    }
}
