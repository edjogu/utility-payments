namespace UtilityPayments.Domain.UtilityPaymentRequiredInfos;

public class UtilityPaymentRequiredInfo
{
    public long Id { get; set; }

    public long UtilityPaymentAgreementId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;

    public RequiredInfoDataType DataType { get; set; }

    public bool IsRequired { get; set; }

    public int DisplayOrder { get; set; }

    public int? MinLength { get; set; }

    public int? MaxLength { get; set; }

    public string? ValidationPattern { get; set; }

    public string? Placeholder { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}
