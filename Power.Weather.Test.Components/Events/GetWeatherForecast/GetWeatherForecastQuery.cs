using Newtonsoft.Json;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Components.Events.GetWeatherForecast;

public class GetWeatherForecastQuery : EventBase<GetWeatherForecastQueryResult>//, ICacheableQuery
{
    [JsonProperty(Required = Required.Default)]
    public bool IncludeInactive { get; set; }
}