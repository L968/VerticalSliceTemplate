﻿using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceTemplate.Application.Domain;
using VerticalSliceTemplate.Application.Domain.Exceptions;

namespace VerticalSliceTemplate.Api.Middleware;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = CreateProblemDetails(exception);

        LogException(
            exception: exception,
            statusCode: problemDetails.Status!.Value,
            traceId: Activity.Current?.Id ?? httpContext.TraceIdentifier
        );

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        if (problemDetails is ValidationProblemDetails validationProblemDetails)
        {
            await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
        }
        else
        {
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        }

        return true;
    }

    private static ProblemDetails CreateProblemDetails(Exception exception)
    {
        return exception switch
        {
            AppException appException => CreateAppExceptionProblemDetails(appException),
            ValidationException validationException => new ValidationProblemDetails
            {
                Title = "Validation Failed",
                Detail = "One or more validation errors occurred.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Status = StatusCodes.Status400BadRequest,
                Errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray())
            },
            _ => new ProblemDetails
            {
                Title = "Internal Server Error",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Status = StatusCodes.Status500InternalServerError,
            }
        };
    }

    private static ProblemDetails CreateAppExceptionProblemDetails(AppException appException)
    {
        return new ProblemDetails
        {
            Title = appException.ErrorType switch
            {
                ErrorType.Failure => "Server Failure",
                ErrorType.Validation => "Validation Error",
                ErrorType.Problem => "Problem Occurred",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict Detected",
                _ => "An unexpected error occurred"
            },
            Detail = appException.Message,
            Type = appException.ErrorType switch
            {
                ErrorType.Failure => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            },
            Status = appException.ErrorType switch
            {
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            },
        };
    }

    private void LogException(Exception exception, int statusCode, string traceId)
    {
        if (statusCode == StatusCodes.Status400BadRequest)
        {
            logger.LogWarning(
                "Status Code: {StatusCode}, TraceId: {TraceId}, Message: {Message} Validation warning occurred",
                statusCode,
                traceId,
                exception.Message
            );
        }
        else
        {
            logger.LogError(
                exception,
                "Status Code: {StatusCode}, TraceId: {TraceId}, Message: {Message}, Error processing request on machine {MachineName}",
                statusCode,
                traceId,
                exception.Message,
                Environment.MachineName
            );
        }
    }
}
