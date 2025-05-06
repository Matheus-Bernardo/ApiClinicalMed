using WebApplication1.Middleware;

namespace WebApplication1.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
            return app.UseMiddleware<ExceptionMiddleware>();
    }
    
}