using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Application.Handlers.GetWeatherForecast;

public class GetWeatherForecastHandler: BaseEventHandler<GetWeatherForecastQuery, GetWeatherForecastResult>
{
    private readonly IWeatherDataService _weatherDataService;

    public GetWeatherForecastHandler(IWeatherDataService weatherDataService)
    {
        _weatherDataService = weatherDataService;
    }

    public override async Task<GetWeatherForecastResult> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var weatherData = await _weatherDataService.GetWeatherForecastAsync(
            new WeatherForecastParam {
                Days = request.Days,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            },
            cancellationToken);
        var result = new GetWeatherForecastResult
        {
            Weather = weatherData
        };
        return result;
    }
}
