using System.Text.RegularExpressions;

namespace Narya.Email.Core.Models;

public sealed class EmailRecipient
{
    private EmailRecipient()
    {
    }

    public EmailRecipient(string email, string? name = null)
    {
        Email = email;
        Name = string.IsNullOrWhiteSpace(name) ? email : name;
    }

    public string Email { get; } = null!;
    public string Name { get; } = null!;

    private static bool IsValidEmail(string email)
    {
        // Regular expression pattern for email validation
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }

    internal Result Validate()
    {
        if (string.IsNullOrWhiteSpace(Email))
            return Result.Failure("Recipient email can't be null or empty.");
        if (!IsValidEmail(Email))
            return Result.Failure($"Email '{Email}' is not valid email address.");

        return Result.Success();
    }
}