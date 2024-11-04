using APIAggregator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubApi _gitHubApi;
        private readonly ILogger<GitHubController> _logger;

        public GitHubController(IGitHubApi gitHubApi, ILogger<GitHubController> logger)
        {
            _gitHubApi = gitHubApi;
            _logger = logger;
        }

        [HttpGet("repositories/{username}")]
        public async Task<IActionResult> GetUserRepositories(string username)
        {
            _logger.LogInformation("Received request to fetch repositories for user: {Username}", username);

            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Invalid username provided");
                return BadRequest("Username cannot be null or empty");
            }

            var repositories = await _gitHubApi.GetUserRepositoriesAsync(username);

            if (repositories is null || repositories.Count == 0)
            {
                _logger.LogInformation("No repositories found for user: {Username}", username);
                return NotFound("No repositories found for the specified user");
            }

            _logger.LogInformation("Successfully fetched {Count} repositories for user: {Username}", repositories.Count, username);
            return Ok(repositories);
        }
    }
}