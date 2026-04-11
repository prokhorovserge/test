using Newtonsoft.Json;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Application.Handlers.GetCurrentWeather;

public class GetCurrentWeatherQuery : EventBase<GetCurrentWeatherResult>//, ICacheableQuery
{
}