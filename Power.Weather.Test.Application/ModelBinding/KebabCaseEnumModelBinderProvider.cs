using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Power.Weather.Test.Application.ModelBinding;

public sealed class KebabCaseEnumModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var modelType = context.Metadata.ModelType;
        var enumType = Nullable.GetUnderlyingType(modelType) ?? modelType;
        if (!enumType.IsEnum)
        {
            return null;
        }

        // Only apply when binding from form (e.g. [FromForm] or multipart form data).
        // For properties of a [FromForm] model, BindingSource is often null; we still want kebab-case conversion.
        var bindingSource = context.BindingInfo.BindingSource?.Id;
        return bindingSource is "Body" or "Path" or "Query" ? null : (IModelBinder)new KebabCaseEnumModelBinder();
    }
}