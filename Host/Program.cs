using System;
using System.Text;
using System.ServiceModel;
using Service.DAL;
using Practice.Core;

namespace Host
{
    class Program
    {
        static ServiceHost _service;
        static void Main(string[] args)
        {            
            Console.WriteLine("Service opened");
            try {
                _service = new ServiceHost(typeof(AnimalService.AnimalServiceImpl));
                _service.Open();
                Console.WriteLine("Started...");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }                       
            Console.ReadKey();
        }
    }
    static class Extension
    {
        public static string FullMessage(this Exception exc)
        {
            var innerExc = exc.InnerException;
            StringBuilder stringBuilder = new StringBuilder();
            while (innerExc!=null) {
                stringBuilder.Append(innerExc.Message);
                stringBuilder.AppendLine();
                innerExc = innerExc.InnerException;
            }
            return stringBuilder.ToString();
        }
    }
}
