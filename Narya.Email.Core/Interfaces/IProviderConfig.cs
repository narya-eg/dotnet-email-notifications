namespace Narya.Email.Core.Interfaces
{
    public interface IProviderConfig
    {
        public bool ValidateProperty(object instance, string propertyName, object? value);
    }
}
