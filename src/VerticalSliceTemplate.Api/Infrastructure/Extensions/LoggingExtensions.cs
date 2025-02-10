using Serilog;

namespace VerticalSliceTemplate.Api.Infrastructure.Extensions;

internal static class LoggingExtensions
{
    public static void AddSerilogLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
