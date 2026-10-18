namespace UtilityPayments.Application.Contracts;

public record AvailableContract(
    long Id,
    string ContractNumber,
    string ProductName,
    string ContractType,
    string Currency);
