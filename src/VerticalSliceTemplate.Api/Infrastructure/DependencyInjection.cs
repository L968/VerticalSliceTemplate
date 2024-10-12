using System.Reflection;
using VerticalSliceTemplate.Api.Infrastructure.Extensions;

namespace VerticalSliceTemplate.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        Config.Init(configuration);

        services.AddDatabase();
        services.AddApplicationServices(assembly);
        services.AddCorsConfiguration();
        services.AddHealthChecksConfiguration();
        services.AddSwaggerConfiguration();

        return services;
    }
}
