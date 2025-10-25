using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;

namespace HighEndStore.API.Middlewares
{
    public class HandleException
    {
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message,
            };

            context.Response.StatusCode = ex switch 
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthenticationException => StatusCodes.Status401Unauthorized,
                ValidationException validationException => HandlevalidationException(validationException,response),
                (_) => StatusCodes.Status500InternalServerError
            };

            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());    
        }

        private int HandlevalidationException(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest;
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
