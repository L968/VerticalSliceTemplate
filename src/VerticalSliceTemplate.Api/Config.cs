namespace VerticalSliceTemplate.Api;

public static class Config
{
    public static string DatabaseConnectionString { get; private set; } = "";
    public static string[] AllowedOrigins { get; private set; } = [];

    public static void Init(IConfiguration configuration)
    {
        DatabaseConnectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException("ConnectionStrings:Database");
        AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ArgumentNullException("AllowedOrigins");
    }
}
