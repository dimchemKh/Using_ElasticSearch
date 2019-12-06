using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Using_Elasticsearch.BusinessLogic.Common.Models;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;

namespace Using_Elastic.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IElasticsearchService _webAppDataService;
        public MainController(IElasticsearchService webAppDataService)
        {
            _webAppDataService = webAppDataService;
        }

        [HttpGet("indexData")]
        public async Task<IActionResult> IndexDataAsync()
        {
            await _webAppDataService.IndexDataAsync();

            return Ok();
        }

        [HttpPost("range")]
        public async Task<IActionResult> GetRangeAsync([FromBody] FilterModel filter)
        {            
            if(filter == null)
            {
                return BadRequest();
            }

            var response = await _webAppDataService.GetRangeAsync(filter);

            return Ok(response);
        }

        [HttpGet("term")]
        public async Task<IActionResult> GetTermAsync([FromBody] FIlterTerm filter)
        {
            if (filter == null)
            {
                return BadRequest();
            }

            var response = await _webAppDataService.GetSearchTermAsync(filter);

            return Ok(response);
        }
    }
}
