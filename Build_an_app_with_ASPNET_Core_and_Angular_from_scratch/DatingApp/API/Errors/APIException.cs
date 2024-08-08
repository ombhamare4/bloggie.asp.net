using System;

namespace API.Errors;

public class APIException(int statusCode, string message, string? details)
{
    public int StatusCode { get; set; } = statusCode;
    
}
