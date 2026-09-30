using Microsoft.EntityFrameworkCore;
using UtilityPayments.Domain.UtilityPaymentAgreements;

namespace UtilityPayments.Infrastructure.Persistence;

public class UtilityPaymentsDbContext(DbContextOptions<UtilityPaymentsDbContext> options)
    : DbContext(options)
{
    public DbSet<UtilityPaymentAgreement> UtilityPaymentAgreements =>
        Set<UtilityPaymentAgreement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UtilityPaymentsDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
