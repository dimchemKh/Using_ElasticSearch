using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.MainScreen.Request;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;
using static Using_Elasticsearch.DataAccess.Entities.Enums.Enums;

namespace Using_Elasticsearch.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MainScreenController : Controller
    {
        private readonly IElasticsearchService _elasticService;
        private readonly IMainScreenService _mainScreenService;
        public MainScreenController(IElasticsearchService elasticService, IMainScreenService mainScreenService)
        {
            _elasticService = elasticService;
            _mainScreenService = mainScreenService;
        }

        [AllowAnonymous]
        [HttpGet("indexData")]
        public async Task<IActionResult> IndexDataAsync()
        {
            await _elasticService.IndexDataAsync();
            await _elasticService.IndexExceptionsAsync();

            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody] RequestSearchMainScreenView filters)
        {
            if(filters == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _mainScreenService.SearchAsync(filters);

            return Ok(response);
        }
        [HttpPost("getFilters")]
        public async Task<IActionResult> GetFiltersAsync([FromBody] RequestGetFiltersMainScreenView filter)
        {
            if (filter == null)
            {
                throw new ProjectException(StatusCodes.Status400BadRequest);
            }

            var response = await _mainScreenService.GetFiltersAsync(filter);

            return Ok(response);
        }

    }
}
