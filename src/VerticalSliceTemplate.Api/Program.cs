using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Globalization;
using VerticalSliceTemplate.Api;
using VerticalSliceTemplate.Api.Behaviours;
using VerticalSliceTemplate.Api.Handlers;
using VerticalSliceTemplate.Api.Infrastructure;
using VerticalSliceTemplate.Api.Infrastructure.Repositories;
using VerticalSliceTemplate.Api.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

Config.Init(builder.Configuration);

var assembly = typeof(Program).Assembly;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddFluentValidationAutoValidation();
ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var serverVersion = ServerVersion.AutoDetect(Config.DatabaseConnectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseMySql(
            Config.DatabaseConnectionString,
            serverVersion,
            mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        )
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(Config.AllowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
