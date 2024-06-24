using Microsoft.AspNetCore.Mvc;
using Narya.Email.Core.Builders;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;
using TestSuite.Models;

namespace TestSuite.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _sendgridService;
    private readonly IEmailService _smtpService;

    public EmailController(IEmailProvider emailProvider)
    {
        _smtpService = emailProvider.GetProvider("Smtp");
        _sendgridService = emailProvider.GetProvider("SendGrid");
    }

    [HttpPost("smtp")]
    public async Task<IActionResult> SendUsingSmtp([FromBody] EmailOptionsModel options)
    {
        var emailOptionsResult =
            new EmailOptionsBuilder(options.To.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithSubject(options.Subject)
                .WithBody(options.Body)
                .WithCc(options.CC.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithBcc(options.BCC.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithAttachment(options.Attachments.ToArray())
                .WithPlaceholder(options.Placeholders.Select(x => new EmailPlaceholder(x.Placeholder, x.Value))
                    .ToArray())
                .RenderAsHtml()
                .Build();

        if (emailOptionsResult.IsFailure)
        {
            return BadRequest(emailOptionsResult.Error);
        }

        var result = await _smtpService.Send(emailOptionsResult.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpPost("sendgrid")]
    public async Task<IActionResult> SendUsingSendgrid([FromBody] EmailOptionsModel options)
    {
        var emailOptionsResult =
            new EmailOptionsBuilder(options.To.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithSubject(options.Subject)
                .WithBody(options.Body)
                .WithCc(options.CC.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithBcc(options.BCC.Select(x => new EmailRecipient(x.Email, x.Name)).ToArray())
                .WithAttachment(options.Attachments.ToArray())
                .WithPlaceholder(options.Placeholders.Select(x => new EmailPlaceholder(x.Placeholder, x.Value))
                    .ToArray())
                .RenderAsHtml()
                .Build();

        if (emailOptionsResult.IsFailure)
        {
            return BadRequest(emailOptionsResult.Error);
        }

        var result = await _sendgridService.Send(emailOptionsResult.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}