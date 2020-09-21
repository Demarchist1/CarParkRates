using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParkRates.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarParkRates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {

        private readonly ILogger<RatesController> _logger;

        public RatesController(ILogger<RatesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<RateResult> Get(DateTime entry, DateTime exit)
        {
            if (exit < entry)
                return BadRequest();

            var rate = RateCalculator.Calculate(entry, exit);

            if(rate == null)
                return BadRequest();

            return Ok(rate);

        }
    }
}
