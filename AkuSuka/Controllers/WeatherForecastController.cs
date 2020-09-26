using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AkuSuka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerManager _logger;
    private IRepositoryWrapper _repoWrapper;

    public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
      _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
      var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
      var owners = _repoWrapper.Owner.FindAll();
      _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            return new string[] { "value1", "value2" };
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    _logger.LogInfo("Here is info message from the controller");
        //    _logger.LogInfo("Here is debug message from the controller");
        //    _logger.LogInfo("Here is info message from the controller");
        //    _logger.LogInfo("Here is info message from the controller");

        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
