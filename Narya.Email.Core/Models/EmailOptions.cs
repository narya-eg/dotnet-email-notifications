using Microsoft.AspNetCore.Http;
using Narya.Email.Core.Extensions;

namespace Narya.Email.Core.Models;

public class EmailOptions
{
    public EmailOptions()
    {
    }

    internal EmailOptions(
        List<EmailRecipientModel> to,
        string subject,
        string body,
        List<EmailRecipientModel> cc,
        List<EmailRecipientModel> bcc,
        List<IFormFile> attachments,
        List<EmailPlaceholderModel> placeholders,
        bool? isBodyHtml = null)
    {
        To = to;
        Subject = subject;
        CC = cc;
        BCC = bcc;
        Attachments = attachments;
        Placeholders = placeholders;
        Body = body.ReplacePlaceholders(Placeholders);
        IsBodyHtml = isBodyHtml ?? body.IsHtml();
    }

    public List<EmailRecipientModel> To { get; set; } = new();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public List<EmailRecipientModel> CC { get; set; } = new();
    public List<EmailRecipientModel> BCC { get; set; } = new();
    public List<IFormFile> Attachments { get; set; } = new();
    public List<EmailPlaceholderModel> Placeholders { get; set; } = new();
    public bool IsBodyHtml { get; set; }
    public string? PlainTextContent => IsBodyHtml ? null : Body;
    public string? HtmlContent => IsBodyHtml ? Body : null;
}