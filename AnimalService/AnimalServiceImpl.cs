using Practice.Core;
using Service.DAL;
using Service.DAL.Context;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AnimalService
{
    [ServiceContract]
    [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    public class AnimalServiceImpl
    {
        private DBAnimal _dbAnimal;
        public AnimalServiceImpl()
        {
            _dbAnimal = new DBAnimal(new AnimalContext());
        }
        [OperationContract]
        public Animal GetById(int id)
        {
            return _dbAnimal.LoadById(id);
        }
        [OperationContract]
        public Animal Save(Animal obj)
        {
            return _dbAnimal.Save(obj);
        }
        [OperationContract]
        public IEnumerable<Animal> Animals()
        {            
            return _dbAnimal.LazyLoadTable();
        }
    }
}
