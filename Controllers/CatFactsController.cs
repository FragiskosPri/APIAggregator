using APIAggregator.Interfaces;
using APIAggregator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatFactsController : ControllerBase
    {
        private readonly ICatFacts _catFactsService;

        public CatFactsController(ICatFacts catFactsService)
        {
            _catFactsService = catFactsService;
        }

        // GET api/catfacts/random/{count}
        [HttpGet("random/{count}")]
        public async Task<ActionResult<List<CatFactsResponse>>> GetRandomCatFacts(int count)
        {
            if (count <= 0)
            {
                return BadRequest("Count must be a positive integer.");
            }

            var catFacts = await _catFactsService.GetRandomCatFactsAsync(count);
            if (catFacts is null)
            {
                return NotFound("No cat facts found.");
            }

            return Ok(catFacts);
        }
    }
}
