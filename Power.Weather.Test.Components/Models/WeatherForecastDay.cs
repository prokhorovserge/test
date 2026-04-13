using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models;

public class WeatherForecastDay: WeatherItem
{
    [JsonProperty(Required = Required.Always)]
    public WeatherItem[] Hours { get; set; } = null!;
}
