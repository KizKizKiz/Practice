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
            animals = new List<Animal>();
            try {
                using (var connection = new SqlConnection(ConfigurationSettings.AppSettings["AnimalSqlProvider"])) {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM Animals", connection);
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var animal = RecordParse(reader);
                            if (animal!=null) {
                                animal.Serialize(reader);                            
                                animals.Add(animal);                                                          
                            }       
                        }
                    }                    
                }
                foreach (var animal in animals) {
                    Console.WriteLine(animal);
                }
            }
            catch (SqlException e) {
                Console.WriteLine($"Source:{e.Source}" +
                    $"\nMessage:{e.Message}");                   
            }         
            
            Console.ReadKey();

        }
        /// <summary>
        /// Преобразует запись объекта <see cref="SqlDataReader"/> в эквивалентный объект <see cref="Animal"/>
        /// </summary>
        /// <param name="reader">Объект, содержащий запись</param>
        static Animal RecordParse(SqlDataReader reader)
        {
            Animal animal = null;
            switch (reader.GetValue(1).ToString()) {
                case "spiders": {
                    animal = new Spider();
                    break;
                }
                case "lepidoptera": {
                    animal = new Butterfly();
                    break;
                }                
            }            
            return animal;
        }
    }
}
