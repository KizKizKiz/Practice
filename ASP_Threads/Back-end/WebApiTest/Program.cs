using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((c) =>
                    {
                        //Uncomment to read settings from database by query
                        //c.AddDbSettings(connection, query);
                        c.AddJsonFile("appsettings.json");
                        c.AddJsonFile("appsettings.Development.json");
                        c.AddEnvironmentVariables();
                        c.AddCommandLine(args);
                    });
                    //Replaced on log4net
                    webBuilder.ConfigureLogging((l) => l.AddLog4Net("log4net.config"));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
