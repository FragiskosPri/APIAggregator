using APIAggregator.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsApi _newsService;
    private readonly ILogger<NewsController> _logger;

    public NewsController(INewsApi newsService, ILogger<NewsController> logger)
    {
        _newsService = newsService;
        _logger = logger;
    }

    [HttpGet("{category}")]
    public async Task<IActionResult> GetTopHeadlines(string category)
    {

        try
        {
            _logger.LogInformation("Received request to get top headlines for category: {Category}", category);

            var articles = await _newsService.GetTopHeadlinesAsync(category);

            if (articles is null)
            {
                _logger.LogWarning("No articles found for category: {Category}", category);
                return NotFound($"No articles found for category '{category}'.");
            }

            return Ok(articles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching top headlines for category: {Category}", category);
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
