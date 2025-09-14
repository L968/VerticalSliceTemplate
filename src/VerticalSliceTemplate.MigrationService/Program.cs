using VerticalSliceTemplate.Application.Infrastructure;
using VerticalSliceTemplate.Application.Infrastructure.Database;
using VerticalSliceTemplate.MigrationService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<AppDbContext>(ServiceNames.PostgresDb);

IHost host = builder.Build();
await host.RunAsync();
