namespace Narya.Email.Core.Models;

public sealed class EmailPlaceholderModel
{
    private EmailPlaceholderModel()
    {
    }

    public EmailPlaceholderModel(string placeholder, string value)
    {
        Placeholder = placeholder;
        Value = value;
    }

    public string Placeholder { get; }
    public string Value { get; }
}