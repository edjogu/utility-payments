using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UtilityPayments.Infrastructure.Persistence;

namespace UtilityPayments.Api.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private const string TestConnectionString =
        "Host=localhost;Port=5432;Database=utility_payments_tests;Username=utility_user;Password=utility_password";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<UtilityPaymentsDbContext>>();
            services.RemoveAll<UtilityPaymentsDbContext>();

            services.AddDbContext<UtilityPaymentsDbContext>(options =>
                options.UseNpgsql(TestConnectionString));
        });
    }
}
