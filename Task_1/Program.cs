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
                var data = collection.Load("SELECT * FROM ANIMALS");

                Console.WriteLine(collection.LoadById(9, "Animals"));
                foreach (var element in data) {
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
