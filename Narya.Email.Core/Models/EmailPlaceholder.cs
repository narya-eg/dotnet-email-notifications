namespace Narya.Email.Core.Models;

public sealed class EmailPlaceholder
{
    private EmailPlaceholder()
    {
    }

    public EmailPlaceholder(string placeholder, string value)
    {
        Placeholder = placeholder;
        Value = value;
    }

    public string Placeholder { get; } = null!;
    public string Value { get; } = null!;
}