using System;

namespace Task_1
{
    class Program
    {                
        static void Main(string[] args)
        {
            try {
                AnimalService.AnimalServiceImplClient client = new AnimalService.AnimalServiceImplClient();
                foreach (var item in client.Animals()) {
                    Console.WriteLine(item.Name);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }                                    
            Console.ReadKey();
        }

    }
}
