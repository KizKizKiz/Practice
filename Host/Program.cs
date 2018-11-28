using System;
using System.Text;
using System.ServiceModel;
using Service.DAL;
using Practice.Core;
using System.Data.Entity;
using Service.DAL.Context;
using System.Collections.Generic;
using System.ServiceModel.Description;
using CommonLib.Extensions;

namespace Host
{
    class Program
    {
        static ServiceHost AnimalServiceHost;
        static void Main(string[] args)
        {
            Console.WriteLine("Try open services...");
            try {               
                AnimalServiceHost = new ServiceHost(typeof(AnimalService.AnimalService));
                AnimalServiceHost.Open();
                Console.WriteLine($"{AnimalServiceHost.Description.Name} has opened");
            }
            catch (Exception e) {
                Console.WriteLine("***** EXCEPTION *****");
                Console.WriteLine(e.FullMessage());
            }                       
            Console.ReadKey();
        }    
    }   
}
