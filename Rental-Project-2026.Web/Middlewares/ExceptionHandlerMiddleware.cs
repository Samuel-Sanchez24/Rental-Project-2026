using Rental_Project_2026.Application.Exceptions;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public const string ERROR_MESSAGE_SESSION_KEY = "ErrorMessage"; 
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "Ha ocurrido un error";

                switch (ex)
                {
                    case BusinessRulesException rulesException:
                        message = rulesException.Message;
                        break;

                    case MediatorException mediatorException:
                        message = mediatorException.Message;
                        break;

                    case CustomValidationException validationEx when validationEx.Errors.Count > 0:
                        message = string.Join("; ", validationEx.Errors);
                        break;

                    case CustomValidationException validationEx:
                        message = validationEx.Message;
                        break;
                }

                await context.Session.LoadAsync(context.RequestAborted);
                context.Session.SetString(ERROR_MESSAGE_SESSION_KEY, message);

                context.Response.Redirect("/Home/Error");
            }
        }
    }

    public static class ExeptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExeptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }   
}
