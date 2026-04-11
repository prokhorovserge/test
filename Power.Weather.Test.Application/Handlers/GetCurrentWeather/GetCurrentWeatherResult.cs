using Newtonsoft.Json;
using Power.Weather.Test.Components.Models;

namespace Power.Weather.Test.Application.Handlers.GetCurrentWeather
{
    public class GetCurrentWeatherResult
    {
        [JsonProperty(Required = Required.Always)]
        public CurrentWeather Weather { get; set; } = null!;
    }
}
