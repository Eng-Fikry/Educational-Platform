using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.ErrorHandel;

namespace Educational_platform.CustomeMiddelwares
{
    public class CustomeExceptionHandler
    {
        private  RequestDelegate _next {  get;  }
        private readonly ILogger<CustomeExceptionHandler> _logger;
        public CustomeExceptionHandler(RequestDelegate Next, ILogger<CustomeExceptionHandler> Logger)
        {
            _next = Next;
            _logger = Logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some Thing Went Wrong");
                ErrorToReturn Response = ErrorHandel(context, ex);

                await context.Response.WriteAsJsonAsync(Response);
            }
        }

        private static ErrorToReturn ErrorHandel(HttpContext context, Exception ex)
        {
            var StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError


            };
            context.Response.StatusCode = StatusCode;
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message,
                StatusCode = context.Response.StatusCode,


            };
            return Response;
        }
    }
}
