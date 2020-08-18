using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UnAuthorization.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var caller = new RequestCaller();
            var request = new MockRequest(){
                Url = "https://5d3a272a-2cbf-4f9a-b733-58dcbcf14633.mock.pstmn.io/test/401",
                Method = "GET"
            };
            caller.getVehicle(request);
            return Ok("OK");
        }


        
    }
}
