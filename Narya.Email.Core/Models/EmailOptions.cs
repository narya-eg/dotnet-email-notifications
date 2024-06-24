using Microsoft.AspNetCore.Http;
using Narya.Email.Core.Extensions;

namespace Narya.Email.Core.Models;

public class EmailOptions
{
    private EmailOptions()
    {
    }

    private EmailOptions(
        List<EmailRecipient> to,
        string subject,
        string body,
        List<EmailRecipient> cc,
        List<EmailRecipient> bcc,
        List<IFormFile> attachments,
        List<EmailPlaceholder> placeholders,
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

    public List<EmailRecipient> To { get; set; } = new();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public List<EmailRecipient> CC { get; set; } = new();
    public List<EmailRecipient> BCC { get; set; } = new();
    public List<IFormFile> Attachments { get; set; } = new();
    public List<EmailPlaceholder> Placeholders { get; set; } = new();
    public bool IsBodyHtml { get; set; }
    public string? PlainTextContent => IsBodyHtml ? null : Body;
    public string? HtmlContent => IsBodyHtml ? Body : null;

    internal static Result<EmailOptions> Create(
        List<EmailRecipient> to,
        string subject,
        string body,
        List<EmailRecipient> cc,
        List<EmailRecipient> bcc,
        List<IFormFile> attachments,
        List<EmailPlaceholder> placeholders,
        bool? isBodyHtml = null)
    {
        foreach (var item in to)
        {
            var res = item.Validate();
            if (res.IsFailure) return Result<EmailOptions>.Failure(res.Error);
        }

        foreach (var item in cc)
        {
            var res = item.Validate();
            if (res.IsFailure) return Result<EmailOptions>.Failure(res.Error);
        }

        foreach (var item in bcc)
        {
            var res = item.Validate();
            if (res.IsFailure) return Result<EmailOptions>.Failure(res.Error);
        }

        var emailOptions = new EmailOptions(to, subject, body, cc, bcc, attachments, placeholders, isBodyHtml);

        return Result<EmailOptions>.Success(emailOptions);
    }
}