using HighEndStore.API.Middlewares;

namespace HighEndStore.API.Extensions
{
    public static class ExceptionHandLingMiddlewareExtensions
    {
        public static WebApplication AddExceptionHandLingMiddleware(this WebApplication app) 
        {
            app.UseMiddleware<GlobalExceptionHandLingMiddleware>();
            return app;
        }
    }
}
