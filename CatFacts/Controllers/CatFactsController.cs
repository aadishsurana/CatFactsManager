using CatFacts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatFacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatFactsController : ControllerBase
    {
        private readonly ICatFactService _cfService;

        public CatFactsController(ICatFactService cfService)
        {
            _cfService = cfService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllFacts()
        {
            var userVoteCollection = await _cfService.ReadAllFacts(@"././AllFacts.json");
            if(userVoteCollection == null)
            {
                return BadRequest();
            }
            return Ok(userVoteCollection);
        }
    }
}
