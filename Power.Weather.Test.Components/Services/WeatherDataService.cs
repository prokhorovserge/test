using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Power.Weather.Test.Application.Constants;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;
using Power.Weather.Test.Components.Resources;

namespace Power.Weather.Test.Components.Services;

public class WeatherDataService : IWeatherDataService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _currentWeatherApiUrl;
    private readonly string _weatherForecastApiUrl;

    public WeatherDataService(IHttpClientFactory httpClientFactory, IOptions<IntegrationConfig> integrationOptions)
    {
        _httpClientFactory = httpClientFactory;
        _currentWeatherApiUrl = integrationOptions.Value.CurrentWeatherApi ?? throw new Exception("Current Weather Api url is not defined");
        _weatherForecastApiUrl = integrationOptions.Value.WeatherForecastApi ?? throw new Exception("Weather Forecast Api url is not defined");
    }

    public async Task<WeatherItem> GetCurrentWeatherAsync(IWeatherParam param, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient(HttpClientConstants.AllowAutoRedirect);

        try
        {
            var url = _currentWeatherApiUrl.Replace("#LAT#", param.Latitude.ToString("0.0000")).Replace("#LON#", param.Longitude.ToString("0.0000"));
            using var response = await client.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                using var content = response.Content;

                return new WeatherItem
                {
                    Date = "",
                    Temperature = 25,
                    Condition = "Sunny"
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

    public async Task<WeatherForecast> GetWeatherForecastAsync(IWeatherForecastParam param, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient(HttpClientConstants.AllowAutoRedirect);

        try
        {
            var url = _weatherForecastApiUrl.Replace("#LAT#", param.Latitude.ToString("0.0000"))
                .Replace("#LON#", param.Longitude.ToString("0.0000"))
                .Replace("#DAYS#", param.Days.ToString());
            using var response = await client.GetAsync(url, cancellationToken);
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

    public static WeatherItem GetWeatherItem(string json)
    {
        try
        {
            var data = JsonConvert.DeserializeObject(json) as JObject;
            if (data == null)
            {
                throw new Exception(StringResources.WeatherApiError);
            }

            return new WeatherItem
            {
                Date = data["date"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                Temperature = data["temperature"]?.ToObject<int>() ?? throw new Exception(StringResources.WeatherJsonError),
                Condition = data["condition"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                ConditionIcon = data["conditionIcon"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                WindSpeed = data["windSpeed"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Pressure = data["pressure"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Humidity = data["humidity"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Cloudy = data["cloudy"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError)
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"{StringResources.WeatherApiError} {ex.Message}");
        }
    }
}
