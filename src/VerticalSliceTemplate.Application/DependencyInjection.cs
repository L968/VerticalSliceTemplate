using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceTemplate.Application.Common.Behaviors;
using VerticalSliceTemplate.Application.Common.Endpoints;
using VerticalSliceTemplate.Application.Common.Extensions;
using VerticalSliceTemplate.Application.Infrastructure;
using VerticalSliceTemplate.Application.Infrastructure.Database;

namespace VerticalSliceTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);

            config.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionStringOrThrow(ServiceNames.PostgresDb);

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            options
                .UseNpgsql(
                    connectionString: dbConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, ServiceNames.DatabaseName)
                )
        );

        services.AddEndpoints(typeof(DependencyInjection).Assembly);

        return services;
    }
}
