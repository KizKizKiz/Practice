using CommonLib.Extensions;
using System;
using System.Linq;
namespace Task_1
{
    class Program
    {                
        static void Main(string[] args)
        {
            try {
                AnimalService.AnimalServiceClient client = new AnimalService.AnimalServiceClient();
                var animal = client.GetByIdFromCache(1);                
                animal.Name = "Dro";                                   
                client.Save(animal, animal.ID);                                
                var an = client.Animals().FirstOrDefault();
                Console.WriteLine(an.Name);
            }
            catch (Exception e) {
                Console.WriteLine(e.FullMessage());
            }                                    
            Console.ReadKey();
        }
    }
}
