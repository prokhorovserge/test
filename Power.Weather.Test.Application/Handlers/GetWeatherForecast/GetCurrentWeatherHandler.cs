using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Components.Services;

namespace Power.Weather.Test.Application.Handlers.GetWeatherForecast
{
    public class GetWeatherForecastHandler: BaseEventHandler<GetWeatherForecastQuery, GetWeatherForecastResult>
    {
        private readonly WeatherDataService _weatherDataService;

        public GetWeatherForecastHandler(WeatherDataService weatherDataService)
        {
            _weatherDataService = weatherDataService;
        }

        public override async Task<GetWeatherForecastResult> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherData = await _weatherDataService.GetWeatherForecastAsync(new WeatherForecastParam { Days = request.Days }, cancellationToken);
            var result = new GetWeatherForecastResult
            {
                Weather = weatherData
            };
            return result;
        }
    }
}
