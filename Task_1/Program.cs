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
        static List<Animal> animals;

        static void Main(string[] args)
        {
            Essential typeEssential = null;
            try 
            {               
                typeEssential = new Essential();

                typeEssential.OpenConnection(ConfigurationSettings.AppSettings["AnimalSqlProvider"]);
                var data = typeEssential.Load("Select * from Animals");
                                
                foreach (var element in data) {
                    Console.WriteLine(element);
                }
            }
            catch (SqlException e) {
                Console.WriteLine($"Source:{e.Source}" +
                    $"\nMessage:{e.Message}");                   
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            finally {
                typeEssential.CloseConnection();
            }
            Console.ReadKey();

        }

    }
}
