using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.Clients;
using UtilityPayments.Domain.Contracts;
using UtilityPayments.Domain.Currencies;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Infrastructure.Persistence;

public class UtilityPaymentsDbContext(DbContextOptions<UtilityPaymentsDbContext> options)
    : DbContext(options)
{
    public DbSet<Client> Clients =>
        Set<Client>();

    public DbSet<ClientType> ClientTypes =>
        Set<ClientType>();

    public DbSet<Contract> Contracts =>
        Set<Contract>();

    public DbSet<ContractProduct> ContractProducts =>
        Set<ContractProduct>();

    public DbSet<ContractStatus> ContractStatuses =>
        Set<ContractStatus>();

    public DbSet<ContractType> ContractTypes =>
        Set<ContractType>();

    public DbSet<CustomerContractRelation> CustomerContractRelations =>
        Set<CustomerContractRelation>();

    public DbSet<CustomerContractRelationType> CustomerContractRelationTypes =>
        Set<CustomerContractRelationType>();

    public DbSet<Currency> Currencies =>
        Set<Currency>();

    public DbSet<UtilityPaymentAgreement> UtilityPaymentAgreements =>
        Set<UtilityPaymentAgreement>();

    public DbSet<UtilityPaymentRequiredInfo> UtilityPaymentRequiredInfos =>
        Set<UtilityPaymentRequiredInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UtilityPaymentsDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
