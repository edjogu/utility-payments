using UtilityPayments.Application.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Api.Endpoints;

public static class UtilityPaymentRequiredInfoEndpoints
{
    public static IEndpointRouteBuilder MapUtilityPaymentRequiredInfoEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/utility-payment-agreements/{agreementId:long}/required-info",
            async (
                long agreementId,
                GetActiveUtilityPaymentRequiredInfos useCase,
                CancellationToken cancellationToken) =>
            {
                var requiredInfos = await useCase.ExecuteAsync(
                    agreementId,
                    cancellationToken);

                return Results.Ok(requiredInfos);
            });

        return app;
    }
}
