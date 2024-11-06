using APIAggregator.Models;
using APIAggregator.Interfaces;

namespace APIAggregator.Services
{
    public class OpenWeatherMapService : IOpenWeatherMap
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenWeatherMapService> _logger; // Injecting logger

        public OpenWeatherMapService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenWeatherMapService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        // Fetch the current weather data for a specified city
        public async Task<WeatherResponse> GetCurrentWeatherAsync(string city)
        {
            var apiKey = _configuration["OpenWeatherMap:ApiKey"];
            var baseUrl = _configuration["OpenWeatherMap:BaseUrl"];
            var url = $"{baseUrl}weather?q={city}&appid={apiKey}&units=metric";

            try
            {
                var weatherResponse = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);

                _logger.LogInformation("Successfully retrieved weather data for city: {City}", city);

                return weatherResponse;
            }
            catch (HttpRequestException e)
            {
                // Log the error and return null
                _logger.LogError(e, "Error retrieving weather data for city: {City}", city);
                return null;
            }
            catch (Exception e)
            {
                // Log any other unexpected exceptions and return null
                _logger.LogError(e, "An unexpected error occurred while fetching weather data.");
                return null;
            }
        }
    }
}
