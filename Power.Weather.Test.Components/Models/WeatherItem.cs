using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models;

public class WeatherItem
{
    [JsonProperty(Required = Required.Always)]
    public string Date { get; set; } = string.Empty;

    [JsonProperty(Required = Required.Always)]
    public double Temperature { get; set; }

    [JsonProperty(Required = Required.Always)]
    public string Condition { get; set; } = string.Empty;

    [JsonProperty(Required = Required.Always)]
    public string ConditionIcon { get; set; } = string.Empty;

    [JsonProperty(Required = Required.Always)]
    public double WindSpeed { get; set; }

    [JsonProperty(Required = Required.Always)]
    public double Pressure { get; set; }

    [JsonProperty(Required = Required.Always)]
    public double Humidity { get; set; }
}
