using WebApi.Middlewares.CustomExceptionsHandling;

namespace WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomExceptionsHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ModelNotFoundExceptionHandlingMiddleware>();
    }
}