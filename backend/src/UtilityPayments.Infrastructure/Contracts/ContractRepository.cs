using Microsoft.EntityFrameworkCore;
using UtilityPayments.Application.Contracts;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Infrastructure.Contracts;

public class ContractRepository(
    UtilityPaymentsDbContext dbContext)
    : IContractRepository
{
    public async Task<IReadOnlyCollection<AvailableContract>> GetAvailableByClientIdAsync(
        long clientId,
        CancellationToken cancellationToken = default)
    {
        return await (
            from relation in dbContext.CustomerContractRelations.AsNoTracking()
            join client in dbContext.Clients
                on relation.ClientId equals client.Id
            join contract in dbContext.Contracts
                on relation.ContractId equals contract.Id
            join product in dbContext.ContractProducts
                on contract.ContractProductId equals product.Id
            join contractType in dbContext.ContractTypes
                on product.ContractTypeId equals contractType.Id
            join contractStatus in dbContext.ContractStatuses
                on contract.ContractStatusId equals contractStatus.Id
            join relationType in dbContext.CustomerContractRelationTypes
                on relation.CustomerContractRelationTypeId equals relationType.Id
            join currency in dbContext.Currencies
                on product.CurrencyId equals currency.Id
            where client.Id == clientId
                && client.IsActive
                && relation.IsActive
                && relationType.Code == "OWNER"
                && contractStatus.Code == "ACTIVE"
                && (
                    contractType.Code == "SAVINGS_ACCOUNT"
                    || contractType.Code == "CURRENT_ACCOUNT"
                )
            orderby contract.ContractNumber
            select new AvailableContract(
                contract.Id,
                contract.ContractNumber,
                product.Name,
                contractType.Code,
                currency.Code)
        ).ToListAsync(cancellationToken);
    }
}
