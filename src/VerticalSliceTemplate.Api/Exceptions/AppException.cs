namespace VerticalSliceTemplate.Api.Exceptions;

#pragma warning disable CA1515
public class AppException(string message) : Exception(message);
