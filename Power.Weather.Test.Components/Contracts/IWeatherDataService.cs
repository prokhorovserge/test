using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Components.Contracts;

public interface IWeatherDataService
{
    Task<WeatherItem> GetCurrentWeatherAsync(IWeatherParam param, CancellationToken cancellationToken);
    Task<WeatherForecast> GetWeatherForecastAsync(IWeatherForecastParam param, CancellationToken cancellationToken);
}
