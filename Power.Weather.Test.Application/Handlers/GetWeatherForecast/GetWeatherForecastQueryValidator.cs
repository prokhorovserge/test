using FluentValidation;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Application.Handlers.GetWeatherForecast;

public class GetWeatherForecastQueryValidator : ValidatorBase<GetWeatherForecastQuery>
{
    public GetWeatherForecastQueryValidator()
    {
        RuleFor(e => e.Latitude)
            .GreaterThanOrEqualTo(-80)
            .LessThanOrEqualTo(80);
        RuleFor(e => e.Longitude)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(180);
        RuleFor(e => e.Days)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(3);
    }
}