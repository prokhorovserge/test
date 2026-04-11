using Newtonsoft.Json;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Components.Events.GetCurrentWeather;

public class GetCurrentWeatherQuery : EventBase<GetCurrentWeatherQueryResult>//, ICacheableQuery
{
    [JsonProperty(Required = Required.Default)]
    public bool IncludeInactive { get; set; }
}