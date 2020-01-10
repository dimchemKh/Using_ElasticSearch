using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Presentation.Common;

namespace Using_Elasticsearch.Presentation.Controllers
{
    [CustomAuthorize]
    [Route("api/[controller]")]
    public class LogsScreenController : Controller
    {
        private readonly ILogsScreenService _logsScreenService;

        public LogsScreenController(ILogsScreenService logsScreenService)
        {
            _logsScreenService = logsScreenService;
        }


        [HttpPost("getLogs")]
        public async Task<IActionResult> GetLogsAsync([FromBody] RequestGetLogsAdminScreenView requestModel)
        {
            if (requestModel == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _logsScreenService.GetLogsAsync(requestModel);

            return Ok(response);
        }
    }
}
