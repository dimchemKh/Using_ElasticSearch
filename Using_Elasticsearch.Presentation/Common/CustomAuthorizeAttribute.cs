
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Using_Elasticsearch.Common.Exceptions;

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
        //readonly Claim _claim;

        public CustomAuthorizeFilter(Claim claim)
        {
            //_claim = claim;
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
