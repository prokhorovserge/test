using FluentValidation;
using FluentValidation.Results;

namespace Power.Weather.Test.Application.Exceptions;

public class ValidationException : Exception
{
    public static void ValidateAndThrow<T>(IValidator<T> validator, T request) where T : class
    {
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    public static async Task ValidateAndThrowAsync<T>(IValidator<T> validator, T request, CancellationToken cancellationToken) where T : class
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string property, params string[] errorMessages) : this()
    {
        Errors[property] = errorMessages;
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }

    public virtual string GetFullErrorMessage()
    {
        if (Errors.Count == 0)
        {
            return base.Message;
        }
        var errorMessages = Errors.SelectMany((e) =>
            new string[] { e.Key }
                .Union(e.Value.Select(m => $"- {m}"))
                .ToArray()
        );

        return $"{base.Message}\n\n{string.Join('\n', errorMessages)}";
    }
}