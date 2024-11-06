using APIAggregator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IOpenWeatherMap _openWeatherMapService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IOpenWeatherMap openWeatherMapService, ILogger<WeatherController> logger)
        {
            _openWeatherMapService = openWeatherMapService;
            _logger = logger;
        }

        // GET: api/weather/current/{city}
        [HttpGet("current/{city}")]
        public async Task<IActionResult> GetCurrentWeather(string city)
        {
            _logger.LogInformation("Received request to fetch weather for city: {City}", city);

            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("Invalid city provided");
                return BadRequest("City cannot be null or empty");
            }

            var weatherData = await _openWeatherMapService.GetCurrentWeatherAsync(city);

            if (weatherData == null)
            {
                _logger.LogInformation("No weather data found for city: {City}", city);
                return NotFound("City not found.");
            }

            _logger.LogInformation("Successfully fetched weather data for city: {City}", city);
            return Ok(weatherData);
        }
    }
}
