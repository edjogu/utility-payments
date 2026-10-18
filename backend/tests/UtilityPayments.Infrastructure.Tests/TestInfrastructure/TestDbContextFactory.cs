using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Infrastructure.Tests.TestInfrastructure;

public static class TestDbContextFactory
{
    public static UtilityPaymentsDbContext Create()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false)
            .Build();

        var connectionString = configuration
            .GetConnectionString("UtilityPaymentsDatabase")
            ?? throw new InvalidOperationException(
                "Connection string 'UtilityPaymentsDatabase' was not found.");

        var options =
            new DbContextOptionsBuilder<UtilityPaymentsDbContext>()
                .UseNpgsql(connectionString)
                .Options;

        return new UtilityPaymentsDbContext(options);
    }
}
