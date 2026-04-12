using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Power.Weather.Test.Components.Models;
using Power.Weather.Test.Components.Services;
using System.Net;
using System.Reflection;

namespace Power.Weather.Test.Tests;

public class WeatherDataServiceTest: BaseTest
{
    private readonly Mock<IHttpClientFactory> _httpFactoryMock = new();
    private readonly Mock<IOptions<IntegrationConfig>> _integrationOptionsMock = new();
    private readonly WeatherDataService _weatherDataService;

    public WeatherDataServiceTest(): base()
    {
        var integrationConfig = new IntegrationConfig
        {
            WeatherApiAddress = "http://api.weatherapi.com/v1/current.json",
            WeatherApiKey = "abcdef",
        };

        _integrationOptionsMock
            .Setup(o => o.Value)
            .Returns(() => integrationConfig);

        _weatherDataService = new WeatherDataService(_httpFactoryMock.Object, _integrationOptionsMock.Object);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_Success()
    {
        var weatherJson = GetWeatherJson("WeatherCurrent");
        SetHttpFactoryMock(weatherJson, "application/json", HttpStatusCode.OK);
        var param = new WeatherParam
        {
            Latitude = 5.7482715,
            Longitude = 37.6235989
        };

        var result = await _weatherDataService.GetCurrentWeatherAsync(param, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal("2026-04-10 17:30", result.Date);
        Assert.Equal(4.2, Math.Round(result.Temperature, 1));
        Assert.Equal("Partly cloudy", result.Condition);
        Assert.Equal("//cdn.weatherapi.com/weather/64x64/day/116.png", result.ConditionIcon);
        Assert.Equal(24.8, Math.Round(result.WindSpeed, 1));
        Assert.Equal(1012.0, Math.Round(result.Pressure, 1));
        Assert.Equal(87, Math.Round(result.Humidity, 1));
        Assert.Equal(75, Math.Round(result.Cloud, 1));
    }

    [Fact]
    public async Task GetWeatherForecastAsync_Success()
    {
        var weatherJson = GetWeatherJson("WeatherForecast");
        SetHttpFactoryMock(weatherJson, "application/json", HttpStatusCode.OK);
        var param = new WeatherForecastParam
        {
            Days = 3,
            Latitude = 5.7482715,
            Longitude = 37.6235989
        };

        var result = await _weatherDataService.GetWeatherForecastAsync(param, CancellationToken.None);
        Assert.NotNull(result);
        var current = result.Current;
        Assert.NotNull(current);
        Assert.Equal("2026-04-10 17:30", current.Date);
        Assert.Equal(4.2, Math.Round(current.Temperature, 1));
        Assert.Equal("Partly cloudy", current.Condition);
        Assert.Equal("//cdn.weatherapi.com/weather/64x64/day/116.png", current.ConditionIcon);
        Assert.Equal(24.8, Math.Round(current.WindSpeed, 1));
        Assert.Equal(1012.0, Math.Round(current.Pressure, 1));
        Assert.Equal(87, Math.Round(current.Humidity, 1));
        Assert.Equal(75, Math.Round(current.Cloud, 1));
        var days = result.Days;
        Assert.NotNull(days);
        Assert.Equal(3, days.Length);
        var day = days[0];
        Assert.NotNull(day);
        var hours = day.Hours;
        Assert.NotNull(hours);
        Assert.Equal(24, hours.Length);
        var hour = hours[0];
        Assert.NotNull(hour);
        Assert.Equal("2026-04-10 00:00", hour.Date);
        Assert.Equal(1.1, Math.Round(hour.Temperature, 1));
        Assert.Equal("Overcast", hour.Condition);
        Assert.Equal("//cdn.weatherapi.com/weather/64x64/night/122.png", hour.ConditionIcon);
        Assert.Equal(23.0, Math.Round(hour.WindSpeed, 1));
        Assert.Equal(1015.0, Math.Round(hour.Pressure, 1));
        Assert.Equal(72, Math.Round(hour.Humidity, 1));
        Assert.Equal(90, Math.Round(hour.Cloud, 1));
    }

    private string GetWeatherJson(string fileName)
    {
        var filePath = $"Power.Weather.Test.Tests.TestData.{fileName}.json";
        var assembly = Assembly.GetExecutingAssembly();
        Assert.NotNull(assembly);
        using var stream = assembly.GetManifestResourceStream(filePath);
        Assert.NotNull(stream);

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return json;
    }

    private void SetHttpFactoryMock(string content, string mediaType, HttpStatusCode statusCode, string? reasonPhrase = null)
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(
                new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content, new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType)),
                    ReasonPhrase = reasonPhrase
                });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);

        _httpFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(() => httpClient);
    }
}
