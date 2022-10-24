using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Toraderkonkurranse.AngularGUI.Controllers;

[ApiController]
[Route("[controller]")]
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

    // Bruker ikke webAPI
    //[HttpGet]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}

    // Bruker webAPI
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        HttpClient client = new HttpClient();
        //Adresse fra swagger
        client.BaseAddress = new Uri("https://localhost:7134/");
        List<WeatherForecast> wf = await client.GetFromJsonAsync<List<WeatherForecast>>("WeatherForecast");
        return wf;
        }
}
