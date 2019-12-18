using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.MainScreen.Request;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;

namespace Using_Elastic.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainScreenController : ControllerBase
    {
        private readonly IElasticsearchService _elasticService;
        private readonly IMainScreenService _mainScreenService;
        public MainScreenController(IElasticsearchService elasticService, IMainScreenService mainScreenService)
        {
            _elasticService = elasticService;
            _mainScreenService = mainScreenService;
        }

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
