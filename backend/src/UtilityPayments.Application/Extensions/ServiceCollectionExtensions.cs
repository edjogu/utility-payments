using Microsoft.Extensions.DependencyInjection;
using UtilityPayments.Application.UtilityPaymentAgreements;

namespace UtilityPayments.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<GetActiveUtilityPaymentAgreements>();

        return services;
    }
}
