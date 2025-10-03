using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaWeatherApi.Models;
using NasaWeatherApi.Services;

namespace NasaWeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] WeatherRequest req)
        {
            try
            {
                var result = await _weatherService.PredictWeatherAsync(req);
                return Ok(new { Status = "Success", Data = result });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Status = "Failed", Message = ex.Message });
            }
        }
        [HttpPost("predict-week")]
        public async Task<IActionResult> PredictWeek([FromBody] WeeklyForecastRequest req)
        {
            try
            {
                var result = await _weatherService.PredictWeeklyWeatherAsync(req);
                return Ok(new { Status="Success",result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Failed", Message = ex.Message });
            }
        }
    }
}
