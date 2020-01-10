using Microsoft.AspNetCore.Http;
using System;
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
        public ErrorHandlingMiddleware(RequestDelegate next, ILogExceptionService exceptionService)
        {
            _next = next;
            _exceptionService = exceptionService;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }
        private async Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception.Message;

            if (exception is ProjectException)
            {
                statusCode = (exception as ProjectException).StatusCode;
            }

            var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            await _exceptionService.Create(exception, context.Request.Path, userId);

            if(statusCode != StatusCodes.Status401Unauthorized && statusCode != StatusCodes.Status403Forbidden 
                && statusCode != StatusCodes.Status400BadRequest && statusCode != StatusCodes.Status404NotFound)
            {
                message = "This server is unavailible";
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(message);
        }
    }
}
