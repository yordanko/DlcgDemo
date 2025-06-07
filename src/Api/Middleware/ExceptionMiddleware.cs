using System;
using System.Text.Json;
using Application.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationException(context, ex);
        }
        catch (AppException appEx)
        {
            await HandleAppException(appEx, context);
        }
        catch (Exception ex)
        {
            // Log the exception (logging logic not shown here)
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = env.IsDevelopment() ? ex.Message : "An unexpected error occurred.",
                Details = env.IsDevelopment() ? ex.StackTrace : null
            };
            await context.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

        }
        finally
        {
            // Any cleanup logic can go here if needed
        }
    }

    private static async Task HandleValidationException(HttpContext context, ValidationException ex)
    {
        var validationErrors = new Dictionary<string, string[]>();

        if (ex.Errors is not null)
        {
            foreach (var error in ex.Errors)
            {
                if (validationErrors.TryGetValue(error.PropertyName, out var existingErrors))
                {
                    validationErrors[error.PropertyName] = [.. existingErrors, error.ErrorMessage];
                }
                else
                {
                    validationErrors[error.PropertyName] = [error.ErrorMessage];
                }
            }
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var validationProblemDetails = new ValidationProblemDetails(validationErrors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation error",
            Detail = "One or more validation errors has occurred"
        };

        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }


    public async Task HandleAppException(AppException appEx, HttpContext context)
    {
        // Log the application-specific exception
        logger.LogError(appEx, appEx.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = appEx.StatusCode;

        var response = new
        {
            StatusCode = appEx.StatusCode,
            Message = env.IsDevelopment() ? appEx.Message : "An error occurred while processing your request.",
            Details = env.IsDevelopment() ? appEx.Details : null
        };
        await context.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
    
    public async Task HandleException(Exception ex, HttpContext context)
    {
        // Log the exception (logging logic not shown here)
        logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = env.IsDevelopment() ? ex.Message : "An unexpected error occurred.",
            Details = env.IsDevelopment() ? ex.StackTrace : null
        };
        await context.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}
