using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataBinding.Core;

namespace DataBinding.Model.DAL.Context
{
    class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AnimalContext>
    {
        /// <summary>
        /// Наполняет <paramref name="context"/> объектами и сохраняет их в базе данных 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(AnimalContext context)
        {
            List<AnimalType> types = new List<AnimalType>() {
                new AnimalType() { Type = SQUAD.spiders },
                new AnimalType() { Type = SQUAD.lepidoptera }
            };
            List<Animal> list = new List<Animal>() {
            new Spider()
            {
                Age = 25,
                Feet = 4,
                HasPoison = true,
                IsDangerous = true,
                IsRare = true,
                Name = "Toss",
                SquadId = types[0].Type,                
            },
            new Spider()
            {
                Age = 11,
                Feet = 4,
                HasPoison = true,
                IsDangerous = true,
                IsRare = true,
                Name = "Affi",
                SquadId = types[0].Type,               
            },
            new Butterfly()
            {
                Age = 9,
                Feet = 2,
                WingsArea = 15,
                IsDangerous = true,
                Color = "Black",
                Name = "Fri",
                SquadId = types[1].Type,                
            },
            new Butterfly()
            {
                Age = 5,
                Feet = 2,
                WingsArea = 12,
                IsDangerous = true,
                Color = "Yellow",
                Name = "Poa",
                SquadId = types[1].Type,                
            }};

            context.Squads.AddRange(types);
            context.Animals.AddRange(list);
            context.SaveChanges();
        }
    }
}
