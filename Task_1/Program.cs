using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Task_1.Core;

namespace Task_1
{
    class Program
    {                
        static void Main(string[] args)
        {
            Essential collection = null;
            try 
            {
                string strConn = ConfigurationSettings.AppSettings["AnimalSqlProvider"];

                collection = new Essential();
                collection.ConnectionString = strConn;
                collection.Table = "Animals";
                Console.WriteLine(collection.LoadById(9));
                var animals = collection.LoadFromCacheByLinq((animal) => animal.Age > 5);
                foreach (var element in animals) {
                    Console.WriteLine(element);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadKey();

        }

    }
}
