using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Extensions;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;
using Narya.Email.Smtp.Extensions;
using Narya.Email.Smtp.Helpers;

namespace Narya.Email.Smtp.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private SmtpConfig _smtpConfig = new();

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Result> Send(EmailOptions options)
    {
        var result = _configuration.GetSmtpConfig();
        if (result.IsFailure) return result;
        _smtpConfig = result.Value;
        await SendEmail(options);
        return Result.Success();
    }

    public async Task<Result> Send(EmailOptions options, dynamic configuration)
    {
        if (configuration is not object)
        {
            return Result.Failure("SMTP configuration is not a valid configurations.");
        }
        Result<SmtpConfig> result = ModelExtension.ConvertTo<SmtpConfig>(configuration);
        if (result.IsFailure) return Result.Failure(result.Errors);
        _smtpConfig = result.Value;
        await SendEmail(options);
        return Result.Success();
    }

    #region Helpers

    private async Task SendEmail(EmailOptions options)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(_smtpConfig.From?.Email!);
        mail.Sender = new MailAddress(_smtpConfig.From?.Email!);
        mail.Subject = options.Subject;
        mail.Body = options.Body;
        mail.IsBodyHtml = options.IsBodyHtml;
        foreach (var item in options.To) mail.To.Add(item.Email);

        if (options.CC.Any())
            foreach (var item in options.CC)
                mail.CC.Add(item.Email);

        if (options.BCC.Any())
            foreach (var item in options.BCC)
                mail.Bcc.Add(item.Email);

        if (options.Attachments.Any())
            foreach (var item in options.Attachments)
                mail.Attachments.Add(item.ToAttachment());

        var smtpClient = new SmtpClient(_smtpConfig.Server, _smtpConfig.Port);
        smtpClient.EnableSsl = _smtpConfig.EnableSsl;
        if (!string.IsNullOrEmpty(_smtpConfig.Password))
            smtpClient.Credentials = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);
        await smtpClient.SendMailAsync(mail);
    }

    #endregion
}