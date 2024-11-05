using APIAggregator.Models;

namespace APIAggregator.Interfaces
{
    public interface IOpenWeatherMap
    {
        Task<WeatherResponse> GetCurrentWeatherAsync(string city);
    }
}
