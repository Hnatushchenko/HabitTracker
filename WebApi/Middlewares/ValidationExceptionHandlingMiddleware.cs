using FluentValidation;

namespace WebApi.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = validationException.Errors.ToDictionary(k => k.PropertyName, v => v.ErrorMessage);
            var message = "Validation failed for the following fields: " + string.Join(", ", errors.Keys);
            await context.Response.WriteAsJsonAsync(new
            {
                Message = message,
                Errors = errors
            });
        }
    }
}