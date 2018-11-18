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
        static ServiceHost SquadServiceHost;
        static AnimalContext Context;
        static void Main(string[] args)
        {
            Console.WriteLine("Try open services...");
            try {
                Context = new AnimalContext();                     
                AnimalServiceHost = new ServiceHost(new AnimalService.AnimalService(Context));
                SquadServiceHost = new ServiceHost(new AnimalService.SquadService(Context));
                TryOpenService(AnimalServiceHost, SquadServiceHost);
            }
            catch (Exception e) {
                Console.WriteLine("***** EXCEPTION *****");
                Console.WriteLine(e.FullMessage());
            }                       
            Console.ReadKey();
        }
        /// <summary>
        /// Переводит массив сервисов в состояние Opened
        /// </summary>
        /// <param name="hosts"></param>
        private static void TryOpenService(params ServiceHost[] hosts)
        {
            foreach (var host in hosts) {
                host.Open();
                Console.WriteLine($"{host.Description.Name} has opened");
            }
        }        
    }
   
}
