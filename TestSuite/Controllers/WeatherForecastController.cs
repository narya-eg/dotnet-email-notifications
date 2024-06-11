using Microsoft.AspNetCore.Mvc;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;
using Narya.Email.Smtp.Interfaces;

namespace TestSuite.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISmtpEmailService _smtpEmailService;
    private readonly IEmailService _emailService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISmtpEmailService smtpEmailService)
    {
        _logger = logger;
        _smtpEmailService = smtpEmailService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
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

    public IActionResult SendUsingSmtp([FromBody] EmailOptions options)
    {
        _smtpEmailService.Send(options);
        _smtpEmailService.Send(options, new Narya.Email.Smtp.Extensions.SmtpConfig
        {

        });
        return Ok();
    }
}