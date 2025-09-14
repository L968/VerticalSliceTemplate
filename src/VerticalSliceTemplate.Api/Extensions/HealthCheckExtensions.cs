using VerticalSliceTemplate.Application.Common.Extensions;
using VerticalSliceTemplate.Application.Infrastructure;

namespace VerticalSliceTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionStringOrThrow(ServiceNames.PostgresDb));

        return services;
    }
}
