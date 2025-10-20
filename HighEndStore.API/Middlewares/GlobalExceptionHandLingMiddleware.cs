namespace HighEndStore.API.Middlewares
{
    public class GlobalExceptionHandLingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandLingMiddleware> _logger;

        public GlobalExceptionHandLingMiddleware(RequestDelegate next,ILogger<GlobalExceptionHandLingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong ==> : {ex.Message}");

                var handler = new HandleException();
                await handler.HandleExceptionAsync(context,ex);  
            }
        }

      
    }
}
