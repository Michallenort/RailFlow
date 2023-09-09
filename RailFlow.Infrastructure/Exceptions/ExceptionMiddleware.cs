using Humanizer;
using Microsoft.AspNetCore.Http;
using Railflow.Core.Exceptions;

namespace RailFlow.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public ExceptionMiddleware()
    {
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await HandleExceptionAsync(e, context);
        }
    }

    public async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException => (StatusCodes.Status400BadRequest,
                new Error(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error."))
        };
        
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
    
    private record Error(string Code, string Reason);
}