using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{

    [Route("api")]
    public class OperationController : ControllerBase
    {
        public readonly ILogger<OperationController> Logger;
        public OperationController(ILogger<OperationController> logger)
        {
            Logger = logger;
        }
        [HttpGet("operation")]
        public async Task<int> LongOperation()
        {
            Logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} Started");
            //Simulate long operation
            await Task.Delay(3000);

            var rnd = new Random();
            return rnd.Next(1000);
        }
        [HttpGet("init")]
        public async Task Initialize()
        {
            Logger.LogInformation($"Initialize Started");

            await Operations.InitializeSystem();
        }
        [HttpGet("cancel")]
        public async Task DoActionWithCancel()
        {
            Logger.LogInformation($"DoActionWithCancel Started");

            await Operations.RunWithTimeout();
        }
        [HttpGet("background")]
        public async Task SomeBackgroundAction()
        {
            Logger.LogInformation($"SomeBackgroundAction Started");

            await Operations.SyncOperations();
        }
    }
}
