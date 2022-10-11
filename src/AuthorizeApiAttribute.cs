using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HawkMiddlewares.Data;

namespace HawkMiddlewares
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeApiAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        

        public AuthorizeApiAttribute()
        {
            
            
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authData = context.HttpContext.Items["X-AUTH"];
            if (authData == null)
            {
                //var response = "401 - Unauthorized!".Unauthorized();

                context.Result = new JsonResult("401") { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }

}


