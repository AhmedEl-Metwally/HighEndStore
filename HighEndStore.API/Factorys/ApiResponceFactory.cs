using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace HighEndStore.API.Factorys
{
    public class ApiResponceFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(E =>E.Value?.Errors.Any() == true) .Select(E => new ValidationError() 
            {
                Field = E.Key,
                Errors = E.Value?.Errors.Select(E =>E.ErrorMessage) ?? new List<string>()
            });

            var response = new ValidationErrorResponse()
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One or more validation error happened"
            };

            return new BadRequestObjectResult(response);    
        }
    }
}
