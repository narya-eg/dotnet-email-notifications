using Microsoft.AspNetCore.Mvc;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

namespace TestSuite.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IEmailService _smtpService;
    private readonly IEmailService _sendgridService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailProvider emailProvider)
    {
        _logger = logger;
        _smtpService = emailProvider.GetProvider("Smtp");
        _sendgridService = emailProvider.GetProvider("SendGrid");
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost("smtp")]
    public IActionResult SendUsingSmtp([FromBody] EmailOptions options)
    {
        _smtpService.Send(options);

        return Ok();
    }

    [HttpPost("sendgrid")]
    public IActionResult SendUsingSendgrid([FromBody] EmailOptions options)
    {
        _sendgridService.Send(options);

        return Ok();
    }
}