using VerticalSliceTemplate.Application;

namespace VerticalSliceTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddMySql(configuration.GetConnectionStringOrThrow("verticalslicetemplate-mysqldb"));

        return services;
    }
}
