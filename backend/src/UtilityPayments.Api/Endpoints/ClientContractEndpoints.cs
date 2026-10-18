using UtilityPayments.Application.Contracts;

namespace UtilityPayments.Api.Endpoints;

public static class ClientContractEndpoints
{
    public static IEndpointRouteBuilder MapClientContractEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clients/{clientId:long}/contracts", async (
            long clientId,
            GetAvailableContracts useCase,
            CancellationToken cancellationToken) =>
        {
            var contracts = await useCase.ExecuteAsync(
                clientId,
                cancellationToken);

            return Results.Ok(contracts);
        });

        return app;
    }
}
