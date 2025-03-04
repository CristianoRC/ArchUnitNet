using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductExample.Api.Controllers;

[ApiController]
[Route("/")]
[Authorize]//TODO: Comentar para falhar o teste
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
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

    // TODO: descomentar para ver falhar
    // [AllowAnonymous]
    [HttpGet("/date")]
    public IActionResult Index()
    {
        return Ok(DateTime.Now);
    }
    
    [HttpGet("/date-utc")]
    public IActionResult Example()
    {
        return Ok(DateTime.UtcNow);
    }
    
    [HttpPost("/date-utc")]
    public IActionResult ExamplePost()
    {
        return Ok(DateTime.UtcNow);
    }
}