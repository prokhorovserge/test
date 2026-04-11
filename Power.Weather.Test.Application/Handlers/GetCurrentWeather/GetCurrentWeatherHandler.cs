using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Components.Services;

namespace Power.Weather.Test.Application.Handlers.GetCurrentWeather
{
    public class GetCurrentWeatherHandler: BaseEventHandler<GetCurrentWeatherQuery, GetCurrentWeatherResult>
    {
        private readonly WeatherDataService _weatherDataService;

        public GetCurrentWeatherHandler(WeatherDataService weatherDataService)
        {
            _weatherDataService = weatherDataService;
        }

        public override async Task<GetCurrentWeatherResult> Handle(GetCurrentWeatherQuery request, CancellationToken cancellationToken)
        {
            var weatherData = await _weatherDataService.GetCurrentWeatherAsync(cancellationToken);
            var result = new GetCurrentWeatherResult
            {
                Weather = weatherData
            };
            return result;
        }
    }
}
