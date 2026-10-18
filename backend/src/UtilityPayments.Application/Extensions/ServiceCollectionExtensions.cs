using Microsoft.Extensions.DependencyInjection;
using UtilityPayments.Application.Contracts;
using UtilityPayments.Application.UtilityPaymentAgreements;
using UtilityPayments.Application.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<GetActiveUtilityPaymentAgreements>();
        services.AddScoped<GetActiveUtilityPaymentRequiredInfos>();
        services.AddScoped<GetAvailableContracts>();

        return services;
    }
}
