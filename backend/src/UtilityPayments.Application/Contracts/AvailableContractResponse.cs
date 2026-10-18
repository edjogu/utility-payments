namespace UtilityPayments.Application.Contracts;

public record AvailableContractResponse(
    long Id,
    string ContractNumber,
    string ProductName,
    string ContractType,
    string Currency);
