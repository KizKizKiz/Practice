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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults =true)]
    public class AnimalService
    {
        private DBAnimal _dbAnimal;
        private DBSquad _dbSquad;
        private AnimalContext _context;
        public AnimalService()
        {
            _context = new AnimalContext();
            _dbAnimal = new DBAnimal(_context);
            _dbSquad = new DBSquad(_context);
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
                    "");
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
        [OperationContract]
        public IEnumerable<SQUAD> Squads()
        {
            return _dbSquad.LazyLoadTable().Select(c => c.Type);
        }
    }
}
