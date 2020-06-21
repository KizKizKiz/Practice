using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace WebApiTest.Controllers
{

    [Route("api")]
    public class OperationController : ControllerBase
    {
        [HttpGet("operation")]
        public async Task<int> LongOperation()
        {
            //Simulate long operation
            await Task.Delay(3000);

            var rnd = new Random();
            return rnd.Next(1000);
        }
    }
}
