using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;

namespace Using_Elasticsearch.Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogExceptionService _exceptionService;
        //private readonly ClaimsPrincipal _claims;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogExceptionService exceptionService)
        {
            _next = next;
            _exceptionService = exceptionService;
            //_claims = claims;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;

            var res = ex as ProjectException;

            var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = Guid.Empty;

            if(Guid.TryParse(userId, out Guid userGuid))
            {
                result = userGuid;
            }

            await _exceptionService.Create(res, context.Request.Path, result);

        }
    }
}
