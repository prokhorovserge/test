using Microsoft.Extensions.Options;
using Power.Weather.Test.Application.Constants;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;
using Power.Weather.Test.Components.Resources;

namespace Power.Weather.Test.Components.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _currentWeatherApi;
        private readonly string _weatherForecastApi;

        public WeatherDataService(IHttpClientFactory httpClientFactory, IOptions<IntegrationConfig> integrationOptions)
        {
            _httpClientFactory = httpClientFactory;
            _currentWeatherApi = integrationOptions.Value.CurrentWeatherApi ?? throw new Exception("Current Weather Api url is not defined");
            _weatherForecastApi = integrationOptions.Value.WeatherForecastApi ?? throw new Exception("Weather Forecast Api url is not defined");
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync(CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient(HttpClientConstants.AllowAutoRedirect);

            try
            {
                using var response = await client.GetAsync(_currentWeatherApi, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    using var content = response.Content;

                    return new CurrentWeather
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TemperatureC = 25,
                        Summary = "Sunny"
                    };
                }
                else
                {
                    throw new Exception($"{StringResources.WeatherApiError} {response.ReasonPhrase}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(WeatherForecastParam param, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient(HttpClientConstants.AllowAutoRedirect);

            try
            {
                using var response = await client.GetAsync(_weatherForecastApi, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    using var content = response.Content;

                    return new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TemperatureC = 25,
                        Summary = "Sunny"
                    };
                }
                else
                {
                    throw new Exception($"{StringResources.WeatherApiError} {response.ReasonPhrase}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
