using System.Text.RegularExpressions;

namespace Narya.Email.Core.Models;

public sealed class EmailRecipientModel
{
    private EmailRecipientModel()
    {
    }

    public EmailRecipientModel(string email, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Recipient email can't be null or empty.");
        if (!IsValidEmail(email)) throw new ArgumentException($"Email '{email}' is not valid email address.");

        Email = email;
        Name = string.IsNullOrWhiteSpace(name) ? email : name;
    }

    public string Email { get; private set; }
    public string Name { get; private set; }

    private static bool IsValidEmail(string email)
    {
        // Regular expression pattern for email validation
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }
}