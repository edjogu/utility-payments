using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Api.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
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

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<UtilityPaymentsDbContext>>();
            services.RemoveAll<UtilityPaymentsDbContext>();

            services.AddDbContext<UtilityPaymentsDbContext>(options =>
                options.UseNpgsql(connectionString));
        });
    }
}
