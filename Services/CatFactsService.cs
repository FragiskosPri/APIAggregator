using APIAggregator.Interfaces;
using APIAggregator.Models;
using System.Text.Json;

namespace APIAggregator.Services
{
    public class CatFactsService : ICatFacts
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CatFactsService> _logger;

        public CatFactsService(HttpClient httpClient, IConfiguration configuration, ILogger<CatFactsService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<CatFactsResponse> GetRandomCatFactsAsync(int count)
        {
            var baseUrl = _configuration["CatFactsApi:BaseUrl"];
            var url = $"{baseUrl}s?limit={count}";

            try
            {
                _logger.LogInformation("Fetching {Count} random cat facts from {Url}", count, url);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var catFacts = await response.Content.ReadFromJsonAsync<CatFactsResponse>();
                if (catFacts is null)
                {
                    _logger.LogWarning("No cat facts found for count: {Count}", count);
                    return null;
                }

                _logger.LogInformation("Successfully fetched {Count} cat facts.", count);
                return catFacts; 
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP request to {Url} failed.", url);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Failed to deserialize JSON response from {Url}.", url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching cat facts.");
            }

            return null;
        }

    }
}
