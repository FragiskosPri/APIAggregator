using APIAggregator.Models;


public interface IOpenWeatherMap
{
    Task<WeatherResponse> GetCurrentWeatherAsync(string city);
}
