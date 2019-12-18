using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;

namespace Using_Elasticsearch.Presentation.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminScreenController : ControllerBase
    {
        private readonly IAdminScreenService _adminService;
        public AdminScreenController(IAdminScreenService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("getLogs")]
        public async Task<IActionResult> GetLogsAsync([FromBody] RequestGetLogsAdminScreenView requestModel)
        {
            if(requestModel == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _adminService.SearchAsync(requestModel);

            return Ok(response);
        }
    }
}
