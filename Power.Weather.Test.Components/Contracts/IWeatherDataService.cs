using Power.Weather.Test.Components.Models;
using Power.Weather.Test.Components.Services;

namespace Power.Weather.Test.Components.Contracts
{
    interface IWeatherDataService
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken);
        Task<WeatherForecast> GetWeatherForecastAsync(WeatherForecastParam param, CancellationToken cancellationToken);
    }
}
