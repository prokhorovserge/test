using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models;

public class Location
{
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = string.Empty;

    [JsonProperty(Required = Required.Always)]
    public string Region { get; set; } = string.Empty;

    [JsonProperty(Required = Required.Always)]
    public string Country { get; set; } = string.Empty;
}
