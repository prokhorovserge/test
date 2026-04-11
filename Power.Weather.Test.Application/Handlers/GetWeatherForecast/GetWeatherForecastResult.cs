using Newtonsoft.Json;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Application.Handlers.GetWeatherForecast
{
    public class GetWeatherForecastResult
    {
        [JsonProperty(Required = Required.Always)]
        public WeatherForecast Weather { get; set; } = null!;
    }
}
