using VerticalSliceTemplate.Api.Infrastructure.Database;
using VerticalSliceTemplate.MigrationService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddMySqlDbContext<AppDbContext>("verticalslicetemplate-mysqldb");

IHost host = builder.Build();
await host.RunAsync();
