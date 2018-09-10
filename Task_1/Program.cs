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
            Essential typeEssential = null;
            try 
            {
                string strConn = ConfigurationSettings.AppSettings["AnimalSqlProvider"];

                typeEssential = new Essential();                
                var data = typeEssential.Load("Select * from Animals", strConn);
                                
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
