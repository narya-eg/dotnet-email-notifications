using Narya.Email.Core.Models;

namespace TestSuite.Models;

public class EmailOptionsModel
{
    public List<EmailRecipientModel> To { get; set; } = new();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public List<EmailRecipientModel> CC { get; set; } = new();
    public List<EmailRecipientModel> BCC { get; set; } = new();
    public List<IFormFile> Attachments { get; set; } = new();
    public List<EmailPlaceholderModel> Placeholders { get; set; } = new();
}

public class EmailRecipientModel
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class EmailPlaceholderModel
{
    public string Placeholder { get; set; } = null!;
    public string Value { get; set; } = null!;
}