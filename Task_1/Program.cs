using System;
using System.Collections.Generic;
using System.Linq;

using Task_1.Core;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>()
            {
                new Insect(5,true),                
                new Butterfly("Red",5.5F)
                {
                    Name = "Bloom",
                    Age = 5,
                    IsDangerous = false
                },
                new Spider(false, true)
                {
                    Name = "Peter Parker",
                    Age = 19,
                    HasPoison = false, 
                    IsDangerous = true,
                    IsRare = true,
                    Feet = 4
                }
            };

            foreach (var animal in animals) {
                Console.WriteLine(animal.ToString());
            }            

            Console.ReadKey();

        }
    }
}
