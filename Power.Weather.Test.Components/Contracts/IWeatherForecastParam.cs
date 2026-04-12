namespace Power.Weather.Test.Components.Contracts;

public interface IWeatherForecastParam: IWeatherParam
{
    public int Days { get; set; }
}
