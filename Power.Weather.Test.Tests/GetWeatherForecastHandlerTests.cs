using Moq;
using Power.Weather.Test.Application.Handlers.GetWeatherForecast;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Tests;

public class GetWeatherForecastHandlerTests: BaseTest
{
    private const int expectedDays = 3;
    private const double expectedLat = 12.34;
    private const double expectedLon = 56.78;
    private readonly Location _location;
    private readonly WeatherItem _weatherItem;
    private readonly WeatherForecast _weatherForecast;
    private readonly GetWeatherForecastQuery _query;
    private readonly Mock<IWeatherDataService> _serviceMock = new();
    private readonly GetWeatherForecastHandler _handler;

    public GetWeatherForecastHandlerTests()
    {
        _location = new Location
        {
            Name = "Москва",
            Region = "Moscow City",
            Country = "Russia"
        };
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
        _weatherForecast = new WeatherForecast
        {
            Location = _location,
            Current = _weatherItem,
            Days =
            [
                new WeatherForecastDay
                {
                    Date = "2026-04-13",
                    Hours = [_weatherItem]
                },
                new WeatherForecastDay
                {
                    Date = "2026-04-14",
                    Hours = [_weatherItem]
                },
                new WeatherForecastDay
                {
                    Date = "2026-04-15",
                    Hours = [_weatherItem]
                }
            ]
        };
        _query = new GetWeatherForecastQuery
        {
            Days = expectedDays,
            Latitude = expectedLat,
            Longitude = expectedLon
        };

        _serviceMock = new Mock<IWeatherDataService>();
        _serviceMock.Setup(x => x.GetWeatherForecastAsync(It.Is<IWeatherForecastParam>(p =>  p.Latitude == expectedLat && p.Longitude == expectedLon && p.Days == expectedDays),
            It.IsAny<CancellationToken>())).ReturnsAsync(_weatherForecast);

        _handler = new GetWeatherForecastHandler(_serviceMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsWeatherResult_WhenServiceReturnsWeather()
    {
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Forecast);
        var location = result.Forecast.Location;
        Assert.Equal(_location.Name, location.Name);
        Assert.Equal(_location.Region, location.Region);
        Assert.Equal(_location.Country, location.Country);
        var current = result.Forecast.Current;
        Assert.Equal(_weatherItem.Date, current.Date);
        Assert.Equal(_weatherItem.Temperature, current.Temperature);
        Assert.Equal(_weatherItem.Condition, current.Condition);
        Assert.Equal(_weatherItem.ConditionIcon, current.ConditionIcon);
        Assert.Equal(_weatherItem.WindSpeed, current.WindSpeed);
        Assert.Equal(_weatherItem.Pressure, current.Pressure);
        Assert.Equal(_weatherItem.Humidity, current.Humidity);

        _serviceMock.Verify(x => x.GetWeatherForecastAsync(It.Is<IWeatherForecastParam>(p => p.Latitude == expectedLat && p.Longitude == expectedLon && p.Days == expectedDays),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_PropagatesException_FromService()
    {
        var weatherServiceMock = new Mock<IWeatherDataService>();
        weatherServiceMock.Setup(x => x.GetWeatherForecastAsync(It.IsAny<IWeatherForecastParam>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("service failure"));

        var handler = new GetWeatherForecastHandler(weatherServiceMock.Object);
        var query = new GetWeatherForecastQuery
        {
            Latitude = 0,
            Longitude = 0
        };

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
    }
}
