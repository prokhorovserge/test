using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models
{
    public class WeatherForecastDay
    {
        [JsonProperty(Required = Required.Always)]
        public string Date { get; set; } = string.Empty;

        [JsonProperty(Required = Required.Always)]
        public WeatherItem[] Hours { get; set; } = null!;
    }
}
