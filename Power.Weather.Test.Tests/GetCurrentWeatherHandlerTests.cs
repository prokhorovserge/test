using Moq;
using Power.Weather.Test.Application.Handlers.GetCurrentWeather;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Tests;

public class GetCurrentWeatherHandlerTests: BaseTest
{
    private const double expectedLat = 12.34;
    private const double expectedLon = 56.78;
    private readonly WeatherItem _weatherItem;
    private readonly GetCurrentWeatherQuery _query;
    private readonly Mock<IWeatherDataService> _serviceMock = new();
    private readonly GetCurrentWeatherHandler _handler;

    public GetCurrentWeatherHandlerTests()
    {
        _weatherItem = new WeatherItem
        {
            Date = "2026-04-12 12:00",
            Temperature = 20.5,
            Condition = "Sunny",
            ConditionIcon = "/icon.png",
            WindSpeed = 10.0,
            Pressure = 1013.0,
            Humidity = 50,
        };
        _query = new GetCurrentWeatherQuery
        {
            Latitude = expectedLat,
            Longitude = expectedLon
        };

        _serviceMock = new Mock<IWeatherDataService>();
        _serviceMock.Setup(x => x.GetCurrentWeatherAsync(It.Is<IWeatherParam>(p => p.Latitude == expectedLat && p.Longitude == expectedLon),
            It.IsAny<CancellationToken>())).ReturnsAsync(_weatherItem);

        _handler = new GetCurrentWeatherHandler(_serviceMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsWeatherResult_WhenServiceReturnsWeather()
    {
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Weather);
        Assert.Equal(_weatherItem.Date, result.Weather.Date);
        Assert.Equal(_weatherItem.Temperature, result.Weather.Temperature);
        Assert.Equal(_weatherItem.Condition, result.Weather.Condition);
        Assert.Equal(_weatherItem.ConditionIcon, result.Weather.ConditionIcon);
        Assert.Equal(_weatherItem.WindSpeed, result.Weather.WindSpeed);
        Assert.Equal(_weatherItem.Pressure, result.Weather.Pressure);
        Assert.Equal(_weatherItem.Humidity, result.Weather.Humidity);

        _serviceMock.Verify(x => x.GetCurrentWeatherAsync(It.Is<IWeatherParam>(p => p.Latitude == expectedLat && p.Longitude == expectedLon),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_PropagatesException_FromService()
    {
        var weatherServiceMock = new Mock<IWeatherDataService>();
        weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<IWeatherParam>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("service failure"));

        var handler = new GetCurrentWeatherHandler(weatherServiceMock.Object);
        var query = new GetCurrentWeatherQuery
        {
            Latitude = 0,
            Longitude = 0
        };

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
    }
}
