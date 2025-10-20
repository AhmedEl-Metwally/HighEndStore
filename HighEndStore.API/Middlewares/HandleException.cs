using Domain.Exceptions;
using Shared.ErrorModels;

namespace HighEndStore.API.Middlewares
{
    public class HandleException
    {
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch 
            {
                NotFoundException => StatusCodes.Status404NotFound,
                (_) => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message,
            }.ToString();
            await context.Response.WriteAsync(response);    
        }

        public async Task HandLeNotFoundApiAsync(HttpContext context)
        {
           context.Request.ContentType = "application/json";    
            var response = new ErrorDetails() 
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = $"The endpoint with {context.Request.Path} not fount"
            }.ToString();  
            await context.Response.WriteAsync(response);
        }

    }

}
