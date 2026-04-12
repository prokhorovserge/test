using Newtonsoft.Json;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Application.Handlers.GetWeatherForecast;

public class GetWeatherForecastQuery : EventBase<GetWeatherForecastResult>//, ICacheableQuery
{
    [JsonProperty(Required = Required.Always)]
    public int Days { get; set; }
    [JsonProperty(Required = Required.Always)]
    public double Latitude { get; set; }
    [JsonProperty(Required = Required.Always)]
    public double Longitude { get; set; }
}