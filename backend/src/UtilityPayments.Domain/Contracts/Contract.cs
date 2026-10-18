namespace UtilityPayments.Domain.Contracts;

public class Contract
{
    public long Id { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public long ContractProductId { get; set; }

    public long ContractStatusId { get; set; }

    public DateOnly ContractDate { get; set; }
}
