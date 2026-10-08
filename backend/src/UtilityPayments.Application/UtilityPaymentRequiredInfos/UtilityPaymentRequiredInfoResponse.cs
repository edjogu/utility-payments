namespace UtilityPayments.Application.UtilityPaymentRequiredInfos;

public record UtilityPaymentRequiredInfoResponse(
    long Id,
    string Code,
    string Label,
    string DataType,
    bool IsRequired,
    int DisplayOrder,
    int? MinLength,
    int? MaxLength,
    string? ValidationPattern,
    string? Placeholder);
