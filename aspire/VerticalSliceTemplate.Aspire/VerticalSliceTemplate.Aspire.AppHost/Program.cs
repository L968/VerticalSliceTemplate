IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> mysqlPassword = builder.AddParameter("mysqlPassword", "root", secret: true);

IResourceBuilder<MySqlServerResource> mysql = builder.AddMySql("verticalslicetemplate-mysql", password: mysqlPassword)
    .WithImageTag("9.2.0")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<MySqlDatabaseResource> mysqldb = mysql.AddDatabase("verticalslicetemplate-mysqldb", "verticalslicetemplate");

builder.AddProject<Projects.VerticalSliceTemplate_Api>("verticalslicetemplate-api")
    .WithReference(mysqldb)
    .WaitFor(mysqldb);

builder.AddProject<Projects.VerticalSliceTemplate_MigrationService>("verticalslicetemplate-migrationservice")
    .WithReference(mysqldb)
    .WaitFor(mysqldb);

await builder.Build().RunAsync();
