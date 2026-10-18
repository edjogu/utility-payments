namespace UtilityPayments.Domain.Contracts;

public class CustomerContractRelation
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public long ContractId { get; set; }

    public long CustomerContractRelationTypeId { get; set; }

    public bool IsActive { get; set; }
}
