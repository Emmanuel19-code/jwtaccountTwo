using jwtaccount_two.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using jwtaccount_two.Service;

namespace jwtaccount_two.Controllers
{
    [Route("/api/Controller")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public  WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        
        [HttpGet("todayweather")]
        public ActionResult<WeatherResponse> GetWeather ()
        {
            var weather = new WeatherResponse
            {
                Date = "2024-2-23",
                Type = "Rainy"
            };
            return weather;
        }

        [HttpPost("check_weather")]
        public ActionResult<WeatherResponse> CheckParticularDay(RequestWeather request)
        {
            var weather = _weatherService.GetParticularDayWeather(request);
            if(weather is null)
            {
                return NotFound();
            }
            return Ok(weather);
        }

        
    }
}