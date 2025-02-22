﻿using Scalar.AspNetCore;

namespace VerticalSliceTemplate.Api.Extensions;

internal static class DocumentationExtensions
{
    public static IApplicationBuilder UseDocumentation(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("VerticalSliceTemplate Api")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);

            options.Servers = [];
        });

        return app;
    }
}
