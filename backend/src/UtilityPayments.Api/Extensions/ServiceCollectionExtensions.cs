namespace UtilityPayments.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("UtilityPaymentsDatabase")
            ?? throw new InvalidOperationException(
                "Connection string 'UtilityPaymentsDatabase' was not found.");

        services.AddCors(options =>
                {
                    options.AddPolicy("Frontend", policy =>
                            {
                                policy
                                .WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                            });
                });

        services.AddHealthChecks()
            .AddNpgSql(connectionString);

        services.AddOpenApi();

        return services;
    }
}
