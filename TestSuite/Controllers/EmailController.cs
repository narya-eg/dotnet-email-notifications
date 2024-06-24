using Microsoft.AspNetCore.Mvc;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

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
    public async Task<IActionResult> SendUsingSmtp([FromBody] EmailOptions options)
    {
        var result = await _smtpService.Send(options);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("sendgrid")]
    public async Task<IActionResult> SendUsingSendgrid([FromBody] EmailOptions options)
    {
        var result = await _sendgridService.Send(options);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}