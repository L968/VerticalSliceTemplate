using VerticalSliceTemplate.Application.Common.Extensions;

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
