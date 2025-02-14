using VerticalSliceTemplate.Api.Domain.Products;
using VerticalSliceTemplate.Api.Infrastructure.Database;
using VerticalSliceTemplate.Api.Infrastructure.Database.Repositories;

namespace VerticalSliceTemplate.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var serverVersion = ServerVersion.AutoDetect(Config.DatabaseConnectionString);

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseMySql(
                    Config.DatabaseConnectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
        );

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }
}
