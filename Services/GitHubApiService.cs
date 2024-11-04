using APIAggregator.Interfaces;
using APIAggregator.Models;
using System.Net.Http.Headers;

namespace APIAggregator.Services
{
    public class GitHubApiService : IGitHubApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GitHubApiService> _logger;

        public GitHubApiService(HttpClient httpClient, IConfiguration configuration, ILogger<GitHubApiService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            // Set the authorization header with the GitHub API token
            var apiKey = _configuration["GitHubApi:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", apiKey);

            // Set the User-Agent header (required by GitHub API)
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApp", "1.0"));

            // Set a reasonable timeout for requests
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<List<Repository>> GetUserRepositoriesAsync(string username)
        {
            var baseUrl = _configuration["GitHubApi:BaseUrl"];
            var url = $"{baseUrl}{username}/repos";

            try
            {
                _logger.LogInformation("Fetching repositories for user: {Username}", username);

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if status code is not successful

                var repositories = await response.Content.ReadFromJsonAsync<List<Repository>>();
                if (repositories is null)
                {
                    _logger.LogWarning("No repositories found for user: {Username}", username);
                    return new List<Repository>();
                }

                return repositories;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP request error: Status Code {StatusCode}, Reason: {Reason}",
                    httpEx.StatusCode, httpEx.Message);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError("Request timed out while fetching repositories for user: {Username}", username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching repositories for user: {Username}", username);
            }

            return new List<Repository>();
        }
    }
}
