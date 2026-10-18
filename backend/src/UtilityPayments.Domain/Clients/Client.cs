namespace UtilityPayments.Domain.Clients;

public class Client
{
    public long Id { get; set; }

    public long ClientTypeId { get; set; }

    public string ClientCode { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
