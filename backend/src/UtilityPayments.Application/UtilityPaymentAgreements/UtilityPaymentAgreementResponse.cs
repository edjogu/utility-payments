namespace UtilityPayments.Application.UtilityPaymentAgreements;

public record UtilityPaymentAgreementResponse(
    long Id,
    string Code,
    string Name,
    string? Description);
