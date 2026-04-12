using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Application.Handlers.GetCurrentWeather;

public class GetCurrentWeatherHandler: BaseEventHandler<GetCurrentWeatherQuery, GetCurrentWeatherResult>
{
    private readonly IWeatherDataService _weatherDataService;

    public GetCurrentWeatherHandler(IWeatherDataService weatherDataService)
    {
        _weatherDataService = weatherDataService;
    }

    public override async Task<GetCurrentWeatherResult> Handle(GetCurrentWeatherQuery request, CancellationToken cancellationToken)
    {
        var weatherData = await _weatherDataService.GetCurrentWeatherAsync(
            new WeatherParam {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            cancellationToken);
        var result = new GetCurrentWeatherResult
        {
            Weather = weatherData
        };
        return result;
    }
}
