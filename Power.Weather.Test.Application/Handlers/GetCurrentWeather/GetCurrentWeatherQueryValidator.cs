using FluentValidation;
using Power.Weather.Test.Application.Events;

namespace Power.Weather.Test.Application.Handlers.GetCurrentWeather;

public class GetCurrentWeatherQueryValidator : ValidatorBase<GetCurrentWeatherQuery>
{
    public GetCurrentWeatherQueryValidator()
    {
        RuleFor(e => e.Latitude)
            .GreaterThanOrEqualTo(-80)
            .LessThanOrEqualTo(80);
        RuleFor(e => e.Longitude)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(180);
    }
}