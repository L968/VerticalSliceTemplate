using VerticalSliceTemplate.Application.Infrastructure;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> postgresPassword = builder.AddParameter("postgresPassword", "root", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres(ServiceNames.Postgres, password: postgresPassword)
    .WithImageTag("17.4")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<PostgresDatabaseResource> postgresdb = postgres.AddDatabase(ServiceNames.PostgresDb, ServiceNames.DatabaseName);

builder.AddProject<Projects.VerticalSliceTemplate_Api>(ServiceNames.Api)
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.VerticalSliceTemplate_MigrationService>(ServiceNames.MigrationService)
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

await builder.Build().RunAsync();
