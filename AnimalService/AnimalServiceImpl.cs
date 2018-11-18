using System;
using Practice.Core;
using Service.DAL;
using Service.DAL.Context;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Data.Entity;

namespace AnimalService
{
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AnimalService
    {
        private DBAnimal _dbAnimal;
        public AnimalService(AnimalContext context)
        {
            _dbAnimal = new DBAnimal(context);
        }
        [OperationContract]
        public Animal GetByIdFromCache(int id)
        {
            Animal animal;
            try {
                animal = _dbAnimal.LoadById(id);
            }
            catch (Exception e) {
                throw new FaultException<ArgumentOutOfRangeException>(
                    new ArgumentOutOfRangeException($"Cannot find record with ID = {id}", e),
                    (string) null);
            }
            return animal;
        }
        [OperationContract]                
        public Animal Save(Animal animal, int id)
        {
            return _dbAnimal.Save(animal, id);
        }
        [OperationContract]
        public IQueryable<Animal> Animals()
        {
            return _dbAnimal.LazyLoadTable();
        }        
    }
}
