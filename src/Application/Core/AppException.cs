using System;

namespace Application.Core;

public class AppException(int statusCode,  string? details) : Exception
{
    public int StatusCode { get; set; } = statusCode;
    public string? Details { get; set; } = details;
}

