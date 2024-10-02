using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseManager.BusinessLogic.Exceptions;

namespace WarehouseManager.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            InvalidPasswordException => (int)HttpStatusCode.BadRequest,
            
            NullReferenceException => (int)HttpStatusCode.NotFound,
            
            ArgumentException argEx when argEx.Message.EndsWith("not found") => (int)HttpStatusCode.NotFound,
            ArgumentException argEx when argEx.Message.EndsWith("error") => (int)HttpStatusCode.InternalServerError,
            
            ArgumentNullException argNullEx when argNullEx.Message.EndsWith("null") => (int)HttpStatusCode.BadRequest
        };

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception switch
            {
                InvalidPasswordException => "Invalid password provided.",
                
                NullReferenceException => "Not found, try other values.",
                
                ArgumentException argEx when argEx.Message.EndsWith("not found") => "Not found, try other values.",
                ArgumentException argEx when argEx.Message.EndsWith("error") => "An unexpected error occurred.",
                
                ArgumentNullException argNullEx when argNullEx.Message.EndsWith("null") => "Bad request, try again."
            },
            Detailed = exception.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
