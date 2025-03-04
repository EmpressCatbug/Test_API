using Microsoft.AspNetCore.Mvc;
using Test_API.Objects;
using Test_API.Repository;
using Test_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeather _weather;
        private readonly IData _data;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeather weather, IData data)
        {
            _logger = logger;
            _weather = weather;
            _data = data;
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
        [HttpGet("Location")]
        public async Task<IActionResult> GetLocation(string place) {
            Location Setting = await _weather.GetLocation(place);
            if (Setting == null) {
                return BadRequest("Location does not exist");
            }
            return Ok(Setting);
        }
    }
}
