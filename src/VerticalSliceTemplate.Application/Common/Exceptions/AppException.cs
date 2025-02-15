using VerticalSliceTemplate.Application.Domain;

namespace VerticalSliceTemplate.Application.Common.Exceptions;

public class AppException(Error error) : Exception(error.Message)
{
    public ErrorType ErrorType { get; } = error.ErrorType;
}
