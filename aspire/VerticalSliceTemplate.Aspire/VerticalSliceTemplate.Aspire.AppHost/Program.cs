IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> postgresPassword = builder.AddParameter("postgresPassword", "root", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("verticalslicetemplate-postgres", password: postgresPassword)
    .WithImageTag("17.4")
    .WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<PostgresDatabaseResource> postgresdb = postgres.AddDatabase("verticalslicetemplate-postgresdb", "verticalslicetemplate");

builder.AddProject<Projects.VerticalSliceTemplate_Api>("verticalslicetemplate-api")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.VerticalSliceTemplate_MigrationService>("verticalslicetemplate-migrationservice")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

await builder.Build().RunAsync();
