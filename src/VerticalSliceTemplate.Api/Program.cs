using Serilog;
using VerticalSliceTemplate.Api.Endpoints;
using VerticalSliceTemplate.Api.Infrastructure;
using VerticalSliceTemplate.Api.Infrastructure.Extensions;
using VerticalSliceTemplate.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddInfrastructure(builder.Configuration, typeof(Program).Assembly);

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
