using UtilityPayments.Api.Extensions;
using UtilityPayments.Infrastructure.Extensions;
using UtilityPayments.Application.Extensions;
using UtilityPayments.Application.UtilityPaymentAgreements;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi(builder.Configuration);

var app = builder.Build();

app.UseApiPipeline();

app.MapGet("/api/utility-payment-agreements", async (
    GetActiveUtilityPaymentAgreements useCase,
    CancellationToken cancellationToken) =>
{
    var agreements = await useCase.ExecuteAsync(cancellationToken);

    return Results.Ok(agreements);
});

app.Run();
