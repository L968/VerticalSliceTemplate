namespace VerticalSliceTemplate.Api.Exceptions;

public class ConfigurationMissingException : Exception
{
    public ConfigurationMissingException(string message) : base(message)
    {
    }
}
