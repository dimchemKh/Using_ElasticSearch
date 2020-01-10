using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

namespace Using_Elasticsearch.Presentation.Common
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { new Claim("", "") };
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        
        public CustomAuthorizeFilter(Claim claim)
        {
        
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //string token = context.HttpContext.Request.Headers["Authorization"];
            if(context.HttpContext.Response.StatusCode == StatusCodes.Status419AuthenticationTimeout)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status419AuthenticationTimeout);

                return;                
            }
        }
    }
}
