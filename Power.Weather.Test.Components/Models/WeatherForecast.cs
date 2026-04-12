using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models;

public class WeatherForecast
{
    [JsonProperty(Required = Required.Always)]
    public WeatherItem Current { get; set; } = null!;

    [JsonProperty(Required = Required.Always)]
    public WeatherForecastDay[] Days { get; set; } = null!;
}
