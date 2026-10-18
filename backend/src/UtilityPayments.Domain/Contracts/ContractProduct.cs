namespace UtilityPayments.Domain.Contracts;

public class ContractProduct
{
    public long Id { get; set; }

    public long ContractTypeId { get; set; }

    public long CurrencyId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
