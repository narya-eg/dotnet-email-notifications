using Microsoft.AspNetCore.Mvc;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

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
    private readonly IEmailService _emailService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
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
        _emailService.Send(options);

        return Ok();
    }
}