﻿using System.Globalization;
using System.Reflection;
using Evently.Common.Application.Behaviors;
using VerticalSliceTemplate.Api.Behaviours;
using VerticalSliceTemplate.Api.Infrastructure.Repositories;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

namespace VerticalSliceTemplate.Api.Infrastructure.Extensions;

internal static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
