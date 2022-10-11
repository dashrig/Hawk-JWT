using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HawkMiddlewares.Data;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HawkMiddlewares
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeApiAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        private readonly JwtOptions _option;

        public AuthorizeApiAttribute()
        {

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var opt = context.HttpContext.RequestServices.GetService<IOptions<JwtOptions>>();

            if (opt == null)
                throw new ArgumentNullException("No Hawk JWT Option supplied, please declare an option for your token & cryptogphy when register Hawk JWT Service `service.AddHawkJwt<TContext>()`!");

            var authData = context.HttpContext.Items[opt?.Value.XAuthHttpContextName];
            if (authData == null)
            {
                //var response = "401 - Unauthorized!".Unauthorized();

                context.Result = new JsonResult("401") { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }

}


