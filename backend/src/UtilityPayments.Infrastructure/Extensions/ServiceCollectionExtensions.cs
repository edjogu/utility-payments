using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtilityPayments.Application.UtilityPaymentAgreements;
using UtilityPayments.Application.UtilityPaymentRequiredInfos;
using UtilityPayments.Infrastructure.Persistence;
using UtilityPayments.Infrastructure.UtilityPaymentAgreements;
using UtilityPayments.Infrastructure.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("UtilityPaymentsDatabase")
            ?? throw new InvalidOperationException(
                "Connection string 'UtilityPaymentsDatabase' was not found.");

        services.AddDbContext<UtilityPaymentsDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUtilityPaymentAgreementRepository, UtilityPaymentAgreementRepository>();

        services.AddScoped<IUtilityPaymentRequiredInfoRepository, UtilityPaymentRequiredInfoRepository>();

        return services;
    }
}
