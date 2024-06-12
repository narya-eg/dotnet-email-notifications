using Narya.Email.Core.Interfaces;
using System.Reflection;

namespace Narya.Email.Core.Extensions
{
    public static class ModelExtension
    {
        public static T ConvertTo<T>(object model) where T : class, IProviderConfig, new()
        {
            T config = new T();
            var configType = typeof(T);

            foreach (var property in configType.GetProperties())
            {
                var modelProperty = model.GetType().GetProperty(property.Name);
                if (modelProperty != null)
                {
                    var value = modelProperty.GetValue(model);
                    if (value != null && property.PropertyType.IsAssignableFrom(modelProperty.PropertyType))
                        property.SetValue(config, value);
                    else
                        SetDefault(config, property);
                }
                else
                    SetDefault(config, property);
            }

            return config;
        }

        private static void SetDefault<T>(T config, PropertyInfo property) where T : class, IProviderConfig, new()
        {
            if (config.RequiredProperty(property.Name))
                throw new ArgumentException($"Missing '{property.Name}' from 'Smtp Configurations'.");
            else
                property.SetValue(config, property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
        }
    }
}
