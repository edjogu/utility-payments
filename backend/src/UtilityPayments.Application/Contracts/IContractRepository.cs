namespace UtilityPayments.Application.Contracts;

public interface IContractRepository
{
    Task<IReadOnlyCollection<AvailableContract>> GetAvailableByClientIdAsync(
        long clientId,
        CancellationToken cancellationToken = default);
}
