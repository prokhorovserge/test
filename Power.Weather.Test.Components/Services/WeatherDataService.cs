using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Power.Weather.Test.Application.Constants;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;
using Power.Weather.Test.Components.Resources;
using System.Timers;

namespace Power.Weather.Test.Components.Services;

public class WeatherDataService : IWeatherDataService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _weatherApiAddress;
    private readonly string _weatherApiKey;

    public WeatherDataService(IHttpClientFactory httpClientFactory, IOptions<IntegrationConfig> integrationOptions)
    {
        _httpClientFactory = httpClientFactory;
        _weatherApiAddress = integrationOptions.Value.WeatherApiAddress ?? throw new Exception("Weather Api host address is not defined");
        _weatherApiKey = integrationOptions.Value.WeatherApiKey ?? throw new Exception("Weather Api key is not defined");
    }

    public async Task<WeatherItem> GetCurrentWeatherAsync(IWeatherParam param, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient(HttpClientConstants.AllowAutoRedirect);

        try
        {
            var url = $"{_weatherApiAddress}?key={_weatherApiKey}&q={param.Latitude.ToString("0.0000000")},{param.Longitude.ToString("0.0000000")}";
            using var response = await client.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                using var content = response.Content;
                using var contentStream = await content.ReadAsStreamAsync(cancellationToken);
                var json = ParseJson(contentStream);
                var current = json["current"] ?? throw new Exception(StringResources.WeatherJsonError);
                return GetWeatherItem(current, "last_updated");
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
            var url = $"{_weatherApiAddress}?key={_weatherApiKey}&q={param.Latitude.ToString("0.0000000")},{param.Longitude.ToString("0.0000000")}&days={param.Days}";
            using var response = await client.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                using var content = response.Content;
                using var contentStream = await content.ReadAsStreamAsync(cancellationToken);
                var json = ParseJson(contentStream);
                var current = json["current"] ?? throw new Exception(StringResources.WeatherJsonError);

                return new WeatherForecast
                {
                    Current = GetWeatherItem(current, "last_updated"),
                    Days = GetForecastWeatherItems(json),
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

    public static JObject ParseJson(Stream stream)
    {
        try
        {
            var serializer = new JsonSerializer();
            using var reader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(reader);
            var data = serializer.Deserialize(jsonTextReader);

            var json = data as JObject;
            return json ?? throw new Exception(StringResources.WeatherJsonError);
        }
        catch (Exception ex)
        {
            throw new Exception(StringResources.WeatherApiError, ex);
        }
    }

    public static WeatherItem GetWeatherItem(JToken json, string dateKey = "time")
    {
        try
        {
            var condition = json["condition"] ?? throw new Exception(StringResources.WeatherJsonError);
            return new WeatherItem
            {
                Date = json[dateKey]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                Temperature = json["temp_c"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Condition = condition["text"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                ConditionIcon = condition["icon"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                WindSpeed = json["wind_kph"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Pressure = json["pressure_mb"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Humidity = json["humidity"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError),
                Cloud = json["cloud"]?.ToObject<double>() ?? throw new Exception(StringResources.WeatherJsonError)
            };
        }
        catch (Exception ex)
        {
            throw new Exception(StringResources.WeatherApiError, ex);
        }
    }

    public static WeatherForecastDay[] GetForecastWeatherItems(JObject json)
    {
        var result = new List<WeatherForecastDay>();
        try
        {
            var forecast = json["forecast"] ?? throw new Exception(StringResources.WeatherJsonError);
            var forecastDays = forecast["forecastday"] ?? throw new Exception(StringResources.WeatherJsonError);
            foreach (var forecastDay in forecastDays)
            {
                var day = new WeatherForecastDay
                {
                    Date = forecastDay["date"]?.ToString() ?? throw new Exception(StringResources.WeatherJsonError),
                };
                var dayHours = new List<WeatherItem>();
                var hours = forecastDay["hour"] ?? throw new Exception(StringResources.WeatherJsonError);
                foreach (var hour in hours)
                {
                    var weatherItem = GetWeatherItem(hour);
                    dayHours.Add(weatherItem);
                }
                day.Hours = [.. dayHours];
                result.Add(day);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(StringResources.WeatherApiError, ex);
        }
        return result.ToArray();
    }
}
