using FluentValidation;

namespace Power.Weather.Test.Application.Events;

public abstract class ValidatorBase<T> : AbstractValidator<T>
    where T : class
{
}
