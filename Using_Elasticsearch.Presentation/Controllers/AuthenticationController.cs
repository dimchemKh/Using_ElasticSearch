using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.Authentification.Request;

namespace Using_Elasticsearch.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentificationService _service;
        public AuthenticationController(IAuthentificationService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] RequestLoginAuthentificationView requestLogin)
        {
            var respose = await _service.LoginAsync(requestLogin);

            throw new ProjectException("Ups");

            return Ok(respose);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            throw new ProjectException("Ups");

            return Ok();
        }
    }
}
