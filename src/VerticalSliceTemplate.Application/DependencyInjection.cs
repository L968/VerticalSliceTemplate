using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceTemplate.Application.Common.Behaviors;
using VerticalSliceTemplate.Application.Common.Endpoints;
using VerticalSliceTemplate.Application.Common.Extensions;
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
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;

        services.AddEndpoints(assembly);

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionStringOrThrow("verticalslicetemplate-postgresdb");

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseNpgsql(
                    connectionString: dbConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "products")
                )
        );

        return services;
    }
}
