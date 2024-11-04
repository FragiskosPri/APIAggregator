using APIAggregator.Interfaces;
using APIAggregator.Models;

public class NewsApiService : INewsApi
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<NewsApiService> _logger;

    public NewsApiService(HttpClient httpClient, IConfiguration configuration, ILogger<NewsApiService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<NewsApiResponse> GetTopHeadlinesAsync(string category)
    {
        var apiKey = _configuration["NewsApi:ApiKey"];
        var baseUrl = _configuration["NewsApi:BaseUrl"];
        string url = $"{baseUrl}top-headlines?category={category}&apiKey={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<NewsApiResponse>();
            if (articles is null)
            {
                _logger.LogWarning("No articles were found under the category: {Category}", category);
                return null;
            }

            return articles;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP request to News API failed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching news articles.");
        }

        return null;
    }

}
