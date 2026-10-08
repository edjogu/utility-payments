using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Infrastructure.Persistence;

public class UtilityPaymentsDbContext(DbContextOptions<UtilityPaymentsDbContext> options)
    : DbContext(options)
{
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
