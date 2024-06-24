using System.Reflection;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

namespace Narya.Email.Core.Extensions;

public static class ModelExtension
{
    public static Result<T> ConvertTo<T>(object model) where T : class, IProviderConfig, new()
    {
        var config = new T();
        var configType = typeof(T);

        foreach (var property in configType.GetProperties())
        {
            Result result;
            var modelProperty = model.GetType().GetProperty(property.Name);
            if (modelProperty is not null) result = SetValue(config, property, modelProperty.GetValue(model));
            else result = SetValue(config, property, null);
            if (result.IsFailure) return Result<T>.Failure(result.Error);
        }

        return Result<T>.Success(config);
    }

    private static Result SetValue<T>(T config, PropertyInfo property, object? value)
        where T : class, IProviderConfig, new()
    {
        if (config.ValidateProperty(config, property.Name, value) is false)
            return Result.Failure($"Invalid '{property.Name}' in 'Smtp Configurations'.");
        property.SetValue(config,
            value is not null ? value :
            property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
        return Result.Success();
    }
}