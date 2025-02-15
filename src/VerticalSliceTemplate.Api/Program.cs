using Serilog;
using VerticalSliceTemplate.Api.Extensions;
using VerticalSliceTemplate.Api.Middleware;
using VerticalSliceTemplate.Application;
using VerticalSliceTemplate.Application.Common.Endpoints;
using VerticalSliceTemplate.Aspire.ServiceDefaults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecksConfiguration(builder.Configuration);

builder.Host.AddSerilogLogging();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseDocumentation();
}

app.MapEndpoints();

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

await app.RunAsync();
