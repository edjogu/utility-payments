using UtilityPayments.Api.Extensions;
using UtilityPayments.Infrastructure.Extensions;
using UtilityPayments.Application.Extensions;
using UtilityPayments.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi(builder.Configuration);

var app = builder.Build();

app.UseApiPipeline();

app.MapEndpoints();

app.Run();
