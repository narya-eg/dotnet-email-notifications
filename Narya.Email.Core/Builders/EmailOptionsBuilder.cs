using Microsoft.AspNetCore.Http;
using Narya.Email.Core.Models;

namespace Narya.Email.Core.Builders;

public class EmailOptionsBuilder
{
    private readonly List<IFormFile> _attachments = new();
    private readonly List<EmailRecipientModel> _bcc = new();
    private readonly List<EmailRecipientModel> _cc = new();
    private readonly List<EmailPlaceholderModel> _placeholders = new();
    private readonly List<EmailRecipientModel> _to;
    private string _body = "(no body)";
    private bool? _isBodyHtml;
    private string _subject = "(no subject)";

    public EmailOptionsBuilder(params EmailRecipientModel[] to)
    {
        _to = to.ToList();
    }

    public EmailOptionsBuilder WithSubject(string subject)
    {
        _subject = subject;
        return this;
    }

    public EmailOptionsBuilder WithBody(string body)
    {
        _body = body;
        return this;
    }

    public EmailOptionsBuilder RenderAsHtml()
    {
        _isBodyHtml = true;
        return this;
    }

    public EmailOptionsBuilder RenderAsClearText()
    {
        _isBodyHtml = false;
        return this;
    }

    public EmailOptionsBuilder WithAttachment(params IFormFile[] files)
    {
        files = files.Where(x => x is not null).ToArray();
        if (files.Any()) _attachments.AddRange(files);
        return this;
    }

    public EmailOptionsBuilder WithCc(params EmailRecipientModel[] email)
    {
        _cc.AddRange(email);
        return this;
    }

    public EmailOptionsBuilder WithBcc(params EmailRecipientModel[] email)
    {
        _cc.AddRange(email);
        return this;
    }

    public EmailOptionsBuilder WithPlaceholder(params EmailPlaceholderModel[] placeholders)
    {
        _placeholders.AddRange(placeholders);
        return this;
    }

    public EmailOptionsBuilder WithPlaceholder(Dictionary<string, string> placeholders)
    {
        var templatePlaceholders = placeholders.Select(x => new EmailPlaceholderModel(x.Key, x.Value));
        _placeholders.AddRange(templatePlaceholders);
        return this;
    }

    public EmailOptions Build()
    {
        return new EmailOptions(
            _to,
            _subject,
            _body,
            _cc,
            _bcc,
            _attachments,
            _placeholders,
            _isBodyHtml
        );
    }
}