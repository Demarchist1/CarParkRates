using CarParkRates.Controllers;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using CarParkRates.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarParkRatesTests
{
    public class RatesTest
    {

        RatesController ratesController;

        // setup
        public RatesTest()
        {
            var LogFactory = LoggerFactory.Create(config => { config.AddConsole(); config.AddDebug(); config.SetMinimumLevel(LogLevel.Trace); });

            ratesController = new RatesController(LogFactory.CreateLogger<RatesController>());
        }

        [Fact]
        public void TestRatesController_Returns_OkStatus()
        {
            var actionResult = ratesController.Get(DateTime.Now, DateTime.Now);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        }

        [Fact]
        public void TestRatesController_Returns_RateResult()
        {
            var actionResult = ratesController.Get(DateTime.Now, DateTime.Now);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);
        }

        [Fact]
        public void TestRatesController_WhenExitIsBeforeEntry_Returns_BadRequest()
        {
            var actionResult = ratesController.Get(DateTime.Now, DateTime.Now.AddDays(-1));

            Assert.IsType<BadRequestResult>(actionResult.Result);

        }

        [Fact]
        public void TestRatesController_EarlyBird_Result()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = new DateTime(2020, 09, 17, 17, 15, 0);  // Thursday afternoon, 5:15pm

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Early Bird", rateResult.Name);
            Assert.Equal(13m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Night_Result()
        {
            var entry = new DateTime(2020, 09, 17, 19, 15, 0);  // Thursday evening, 7:15pm
            var exit = new DateTime(2020, 09, 18, 5, 15, 0);  // Friday morning, 5:15am

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Night", rateResult.Name);
            Assert.Equal(6.5m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Weekend_Result()
        {
            var entry = new DateTime(2020, 09, 19, 19, 15, 0);  // Saturday evening, 7:15pm
            var exit = new DateTime(2020, 09, 20, 5, 15, 0);  // Sunday morning, 5:15am

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Weekend", rateResult.Name);
            Assert.Equal(10m, rateResult.Rate);
        }


        [Fact]
        public void TestRatesController_Standard_UnderOneHour()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddMinutes(45);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(5m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Standard_OverOneHour()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddHours(1).AddMinutes(10);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(10m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Standard_OverTwoHours()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddHours(2).AddMinutes(10);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(15m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Standard_OverThreeHours()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddHours(3).AddMinutes(10);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(20m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Standard_OverSixHours()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddHours(6).AddMinutes(10);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(20m, rateResult.Rate);
        }

        [Fact]
        public void TestRatesController_Standard_OverOneDay()
        {
            var entry = new DateTime(2020, 09, 17, 7, 15, 0);  // Thursday morning, 7:15am
            var exit = entry.AddDays(1).AddMinutes(10);

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(40m, rateResult.Rate);
        }



        // Note: If a customer enters the carpark before midnight on Friday 
        // and if they qualify for Night rate on a Saturday morning, 
        // then the program should charge the night rate instead of weekend rate.  

        // Ben's comment - These 2 rates do not overlap, so not sure how to test
        // Night = Before Midnight
        // Weekend = After Midnight
        [Fact]
        public void TestRatesController_EdgeCase_NightOrWeekend()
        {
            var entry = new DateTime(2020, 09, 18, 21, 15, 0);  // Friday Night, 9:15pm
            var exit = new DateTime(2020, 09, 19, 7, 15, 0);  // Saturday morning, 7:15am

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Night", rateResult.Name);
            Assert.Equal(6.5m, rateResult.Rate);
        }



        // Note: The customer should get the cheapest deal based on the rules which apply to the time period.

        // Ben's comment - based on this, a short stay on the weekend should attract the standard rate, not the weekend rate
        [Fact]
        public void TestRatesController_EdgeCase_UnderOneHourOnTheWeekend()
        {
            var entry = new DateTime(2020, 09, 19, 21, 15, 0);  // Saturday Night, 9:15pm
            var exit = entry.AddMinutes(30); 

            var actionResult = ratesController.Get(entry, exit);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var rateResult = Assert.IsType<RateResult>(okResult.Value);

            Assert.Equal("Standard", rateResult.Name);
            Assert.Equal(5m, rateResult.Rate);
        }


    }
}
