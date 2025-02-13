namespace VerticalSliceTemplate.Api.Domain;

public sealed record Error(
    string Message,
    ErrorType ErrorType
);
