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
                    webBuilder.ConfigureAppConfiguration((context, builder) =>
                    {
                        var config = builder.Build();
                        builder.AddDbSettings<SqlConnection>(config.GetConnectionString("ConnectionString"), 
                            "select * from Settings");
                    });
                    //Replaced on log4net
                    webBuilder.ConfigureLogging((l) => l.AddLog4Net("log4net.config"));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
