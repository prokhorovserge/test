using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Components.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        //private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;

        public async Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(WeatherForecastParam param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
