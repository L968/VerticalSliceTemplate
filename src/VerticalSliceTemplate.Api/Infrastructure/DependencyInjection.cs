using System.Reflection;
using VerticalSliceTemplate.Api.Endpoints;
using VerticalSliceTemplate.Api.Infrastructure.Extensions;

namespace VerticalSliceTemplate.Api.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        Config.Init(configuration);

        services.AddDatabase();
        services.AddApplicationServices(assembly);
        services.AddEndpoints(assembly);
        services.AddCorsConfiguration();
        services.AddHealthChecksConfiguration();
        services.AddDocumentation();

        return services;
    }
}
