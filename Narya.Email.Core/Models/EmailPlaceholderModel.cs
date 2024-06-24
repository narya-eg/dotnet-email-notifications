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

    public string Placeholder { get; } = null!;
    public string Value { get; } = null!;
}