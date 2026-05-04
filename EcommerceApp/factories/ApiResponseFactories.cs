using Microsoft.AspNetCore.Mvc;
using Shared.ErrorsModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApp.factories
{
    public static class  ApiResponseFactories
    {
        public static IActionResult GenerateErrorValidationResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(entry => entry.Value.Errors.Any())
        .Select(entry => new ValidationErrors
        {
            Field = entry.Key,
            Errors = entry.Value.Errors.Select(error => error.ErrorMessage)
        });

            var errorResponse = new ModelStateErrors
            {
                Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
        }
    }
}
