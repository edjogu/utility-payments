using UtilityPayments.Application.UtilityPaymentAgreements;

namespace UtilityPayments.Api.Endpoints;

public static class UtilityPaymentAgreementEndpoints
{
    public static IEndpointRouteBuilder MapUtilityPaymentAgreementEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/utility-payment-agreements", async (
            GetActiveUtilityPaymentAgreements useCase,
            CancellationToken cancellationToken) =>
        {
            var agreements = await useCase.ExecuteAsync(cancellationToken);

            return Results.Ok(agreements);
        });

        return app;
    }
}
