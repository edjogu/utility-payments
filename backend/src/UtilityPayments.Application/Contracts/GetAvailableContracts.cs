namespace UtilityPayments.Application.Contracts;

public class GetAvailableContracts
{
    private readonly IContractRepository _contractRepository;

    public GetAvailableContracts(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<IReadOnlyCollection<AvailableContractResponse>> ExecuteAsync(
        long clientId,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<AvailableContract> contracts =
            await _contractRepository.GetAvailableByClientIdAsync(
                clientId,
                cancellationToken);

        return contracts
            .Select(contract => new AvailableContractResponse(
                contract.Id,
                contract.ContractNumber,
                contract.ProductName,
                contract.ContractType,
                contract.Currency))
            .ToList();
    }
}
