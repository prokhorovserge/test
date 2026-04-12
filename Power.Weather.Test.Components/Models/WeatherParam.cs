using Power.Weather.Test.Components.Contracts;

namespace Power.Weather.Test.Components.Models;

public class WeatherParam: IWeatherParam
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
