namespace VerticalSliceTemplate.Api;

internal static class Config
{
    public static string DatabaseConnectionString { get; private set; } = "";
    public static string[] AllowedOrigins { get; private set; } = [];

    public static void Init(IConfiguration configuration)
    {
        DatabaseConnectionString = configuration.GetConnectionString("Database") ?? throw new MissingConfigurationException("ConnectionStrings:Database");
        AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new MissingConfigurationException("AllowedOrigins");
    }
}
