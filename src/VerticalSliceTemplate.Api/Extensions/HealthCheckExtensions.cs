using VerticalSliceTemplate.Api.Infrastructure.Database;

namespace VerticalSliceTemplate.Api.Extensions;

internal static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

        return services;
    }
}
