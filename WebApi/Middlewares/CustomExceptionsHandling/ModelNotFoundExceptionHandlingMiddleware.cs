using Domain.Exceptions;

namespace WebApi.Middlewares.CustomExceptionsHandling;

public sealed class ModelNotFoundExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ModelNotFoundExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ModelNotFoundException modelNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "The resource with the specified identifier was not found.",
                Details = new
                {
                    Id = modelNotFoundException.ModelId
                }
            });
        }
    }
}