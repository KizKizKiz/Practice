using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Task_1.Core;
using Task_1.DAL;
namespace Task_1
{
    class Program
    {                
        static List<Animal> animals;

        static void Main(string[] args)
        {
            AnimalDAL<Spider> dAL = null;
            try 
            {
                dAL = new AnimalDAL<Spider>();
                dAL.OpenConnection(ConfigurationSettings.AppSettings["AnimalSqlProvider"]);
                var animals = dAL.Load("Select * from Animals");
                foreach (var animal in animals) {
                    Console.WriteLine(animal);
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
                dAL.CloseConnection();
            }
            Console.ReadKey();

        }

    }
}
