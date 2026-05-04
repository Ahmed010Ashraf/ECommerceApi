using Domain.Exceptions;
using Shared.ErrorsModels;
using System.Net;
using System.Text.Json;

namespace EcommerceApp.CustomExceptionMiddleware
{
    public class CustomExceptionMiddlewareHandeller
        (RequestDelegate _next , ILogger<CustomExceptionMiddlewareHandeller> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var res = new ErrorDetails()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = $"the end point with this path {context.Request.Path} is not found"
                    };
                    //await context.Response.WriteAsync(JsonSerializer.Serialize(res) );
                    await context.Response.WriteAsJsonAsync(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error here");

                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message,
                };

                response.StatusCode = ex switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    InvalidPasswordException => (int)HttpStatusCode.Unauthorized,
                    badRequestException badRequestException => GenerateResponse(badRequestException , response),
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);

            }
        }

        private int GenerateResponse(badRequestException badRequestException, ErrorDetails response)
        {
            response.Details = badRequestException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
