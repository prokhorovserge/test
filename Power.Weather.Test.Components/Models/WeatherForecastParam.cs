using Power.Weather.Test.Components.Contracts;

namespace Power.Weather.Test.Components.Models;

public class WeatherForecastParam: WeatherParam, IWeatherForecastParam
{
    public int Days { get; set; }
}
