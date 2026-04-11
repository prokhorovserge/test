using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Power.Weather.Test.Application.ModelBinding;

public sealed class KebabCaseEnumModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var modelType = bindingContext.ModelMetadata.ModelType;
        var enumType = Nullable.GetUnderlyingType(modelType) ?? modelType;
        if (!enumType.IsEnum)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var pascalCase = KebabToPascalCase(value);

        if (Enum.TryParse(enumType, pascalCase, ignoreCase: true, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
            $"The value '{value}' is not valid for {bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName}.");

        bindingContext.Result = ModelBindingResult.Failed();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Converts kebab-case to PascalCase (e.g. "stack-adapt" -> "StackAdapt", "e-newsletter" -> "ENewsletter").
    /// </summary>
    internal static string KebabToPascalCase(string kebabCase)
    {
        if (string.IsNullOrEmpty(kebabCase))
        {
            return kebabCase;
        }

        var parts = kebabCase.Split('-');
        return string.Concat(
            parts.Select(part => part.Length == 0 ? part : char.ToUpperInvariant(part[0]) + part[1..].ToLowerInvariant())
        );
    }
}