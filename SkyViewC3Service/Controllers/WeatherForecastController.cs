using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RobotStoreEntitiesLib;
using SkyViewC3Service.Utils;

namespace SkyViewC3Service.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetSessionKey()
        {
            HttpContext.Session.Set<User>("CurrentUser", new User { UserID = "11", Name = "jiho", Password = "123", Email = "jihoson94@rnd.re.kr" });
            var user = HttpContext.Session.Get<User>("CurrentUser");
            return Ok(user);
        }
    }
}
