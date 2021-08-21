using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
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

        private readonly IListingExcutor _listingExcutor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, EngineFactory engineFactory)
        {
            _logger = logger;
            _listingExcutor = engineFactory.Create<AeEngine>();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _listingExcutor.Excute("1");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet("test")]
        //public IActionResult Get([FromBody] RequestTest requestTest)
        //{
        //    return Ok(requestTest);
        //}

        //public class RequestTest
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}
    }
}
