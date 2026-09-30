namespace UtilityPayments.Domain.UtilityPaymentAgreements;

public class UtilityPaymentAgreement
{
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string IntegrationKey { get; set; } = string.Empty;

    public bool AllowInformationRequest { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}
