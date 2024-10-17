﻿using Scalar.AspNetCore;

namespace VerticalSliceTemplate.Api.Infrastructure.Extensions;

internal static class DocumentationExtensions
{
    public static IServiceCollection AddDocumentationConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseDocumentation(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "openapi/{documentName}.json";
        });

        app.MapScalarApiReference(options => {
            options
                .WithTitle("VerticalSliceTemplate API")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        return app;
    }
}
