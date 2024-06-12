using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Extensions;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;
using Narya.Email.Sendgrid.Exceptions;
using Narya.Email.Sendgrid.Extensions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Narya.Email.Sendgrid.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private SendGridConfig _sendGridConfig = new();

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Send(EmailOptions options)
    {
        _sendGridConfig = _configuration.GetSendGridConfig();
        await SendEmail(options);
    }
    public async Task Send(EmailOptions options, dynamic configuration)
    {
        if (configuration is not object) throw new Exception("SendGrid configuration is not a valid configurations.");
        _sendGridConfig = ModelExtension.ConvertTo<SendGridConfig>(configuration);
        await SendEmail(options);
    }
    private async Task SendEmail(EmailOptions options)
    {
        var client = new SendGridClient(_sendGridConfig.ApiKey);
        var from = new EmailAddress(_sendGridConfig.From?.Email, _sendGridConfig.From?.Name);

        // Multiple
        var to = options.To.Select(x => new EmailAddress(x.Email, x.Name)).ToList();
        var cc = options.CC.Select(x => new EmailAddress(x.Email, x.Name)).Where(c => !to.Contains(c)).ToList();
        var bcc = options.BCC.Select(x => new EmailAddress(x.Email, x.Name)).Where(c => !to.Contains(c)).ToList();
        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, options.Subject,
            options.PlainTextContent, options.HtmlContent);

        if (cc.Any())
            msg.AddCcs(cc);

        if (bcc.Any())
            msg.AddBccs(bcc);

        if (options.Attachments.Any())
            foreach (var item in options.Attachments)
            {
                using var ms = new MemoryStream();
                await item.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                var file = Convert.ToBase64String(fileBytes);
                msg.AddAttachment(item.FileName, file);
            }

        var response = await client.SendEmailAsync(msg);
        await SendGridException.ThrowExceptionOnError(response);
    }
}