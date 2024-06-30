using System.ComponentModel.DataAnnotations;

namespace Narya.Email.Core.Interfaces;

public interface IProviderConfig
{
    public bool ValidateProperty(object instance, string propertyName, object? value);
    public bool ValidateObject(object instance, out List<ValidationResult> validationResults);
}