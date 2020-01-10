using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Presentation.Common;

namespace Using_Elasticsearch.Presentation.Controllers
{
    //[Authorize]
    [CustomAuthorize]
    [Route("api/[controller]")]
    public class AdminScreenController : Controller
    {
        private readonly IAdminScreenService _adminService;
        public AdminScreenController(IAdminScreenService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("getUsers")]
        public async Task<IActionResult> GetUsersAsync([FromBody] RequestGetUsersAdminScreenView requestModel)
        {
            if (requestModel == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _adminService.GetUsersAsync(requestModel);

            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RequestCreateUserAdminScreenView requestModel)
        {
            if (requestModel == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _adminService.CreateUserAsync(requestModel);

            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] RequestCreateUserAdminScreenView requestModel)
        {
            if (requestModel == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            //await _adminService.requestModel
            return Ok();

        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveUserAsync([FromBody] string userId)
        {
            if (userId == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            return Ok();
        }
    }
}
