using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Task_1.DAL.Context;
using Task_1.Core;
using System.Data.Entity.Core;

namespace Task_1
{
    class Program
    {                
        static void Main(string[] args)
        {            
            try 
            {
                DBAnimal essential = new DBAnimal();
                foreach (var item in essential.Load()) {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
                        
            Console.ReadKey();
        }

    }
}
