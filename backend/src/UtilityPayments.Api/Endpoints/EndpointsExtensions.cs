namespace UtilityPayments.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapUtilityPaymentAgreementEndpoints();
        app.MapUtilityPaymentRequiredInfoEndpoints();

        return app;
    }
}
