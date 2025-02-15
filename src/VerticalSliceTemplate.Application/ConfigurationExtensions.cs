using Microsoft.Extensions.Configuration;

namespace VerticalSliceTemplate.Application;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ??
               throw new MissingConfigurationException($"The connection string {name} was not found");
    }

    public static T GetValueOrThrow<T>(this IConfiguration configuration, string name)
    {
        return configuration.GetValue<T?>(name) ??
               throw new MissingConfigurationException($"The configuration key {name} was not found");
    }
}
