using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rental_Project_2026.Application.UseCases.Account.Queries.UserHasPermission;
using System.Security.Claims;
namespace Rental_Project_2026.Web.Security
{
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(string permissionCode) : base(typeof(RequirePermissionFilter))
        {
            Arguments = new object[] { permissionCode };
        }
    }

    public sealed class RequirePermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permissionCode;    
        private readonly IMediator _mediator;

        public RequirePermissionFilter(string permissionCode, IMediator mediator)
        {
            _permissionCode = permissionCode;
            _mediator = mediator;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            HttpContext httpContext = context.HttpContext;

            if (httpContext.User?.Identity?.IsAuthenticated != true) 
            { 
                context.Result = new RedirectToRouteResult(                  
                    new RouteValueDictionary
                    {
                        ["controller"] = "Account",
                        ["action"] = "Login",
                        ["returnUrl"] = httpContext.Request.Path

                    });
                return;

            }

            string? userId = httpContext.User.FindFirstValue (ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) 
            { 
                context.Result = new ForbidResult();
                return;
            }

            bool hasPermission = await _mediator.Send(new UserHasPermissionQuery
            {
                UserId = userId,
                PermissionCode = _permissionCode

            });
            if (hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}