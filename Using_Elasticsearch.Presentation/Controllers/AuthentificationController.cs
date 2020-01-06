using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Views.Authentification.Request;

namespace Using_Elasticsearch.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AuthentificationController : Controller
    {
        private readonly IAuthentificationService _service;
        private readonly IJwtFactoryHelper _jwtFactory;
        public AuthentificationController(IAuthentificationService service, IJwtFactoryHelper jwtFactory)
        {
            _service = service;
            _jwtFactory = jwtFactory;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] RequestLoginAuthentificationView requestLogin)
        {
            var respose = await _service.LoginAsync(requestLogin);

            return Ok(respose);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RequestRefreshAuthentificationView requestRefresh)
        {
            var email = _jwtFactory.ValidateToken(requestRefresh.RefreshToken);

            var user = await _service.FindUserAsync(email);

            var response = _jwtFactory.Generate(user);

            return Ok(response);
        }
    }
}
