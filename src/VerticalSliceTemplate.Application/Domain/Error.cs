namespace VerticalSliceTemplate.Application.Domain;

public sealed record Error(
    string Message,
    ErrorType ErrorType
);
