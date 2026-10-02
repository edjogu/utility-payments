using Scalar.AspNetCore;

namespace UtilityPayments.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApiPipeline(
        this WebApplication app)
    {
        app.UseCors("Frontend");

        app.MapHealthChecks("/health");
        app.MapOpenApi();
        app.MapScalarApiReference();

        return app;
    }
}
