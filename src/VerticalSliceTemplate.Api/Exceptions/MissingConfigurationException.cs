namespace VerticalSliceTemplate.Api.Exceptions;

#pragma warning disable CA1515
public class MissingConfigurationException(string message) : Exception(message);
