using System.Reflection;
using Narya.Email.Core.Interfaces;

namespace Narya.Email.Core.Extensions;

public static class ModelExtension
{
    public static T ConvertTo<T>(object model) where T : class, IProviderConfig, new()
    {
        var config = new T();
        var configType = typeof(T);

        foreach (var property in configType.GetProperties())
        {
            var modelProperty = model.GetType().GetProperty(property.Name);
            if (modelProperty is not null) SetValue(config, property, modelProperty.GetValue(model));
            else SetValue(config, property, null);
        }

        return config;
    }

    private static void SetValue<T>(T config, PropertyInfo property, object? value)
        where T : class, IProviderConfig, new()
    {
        if (config.ValidateProperty(config, property.Name, value) is false)
            throw new ArgumentException($"Invalid '{property.Name}' in 'Smtp Configurations'.");
        property.SetValue(config,
            value is not null ? value :
            property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
    }
}