using VerticalSliceTemplate.Api.Domain;

namespace VerticalSliceTemplate.Api.Exceptions;

public class AppException(Error error) : Exception(error.Message)
{
    public ErrorType ErrorType { get; } = error.ErrorType;
}
