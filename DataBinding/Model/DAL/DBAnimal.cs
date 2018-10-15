using System;
using System.Data.Entity;
using System.Data.SqlClient;
using DataBinding.Core;
using DataBinding.Model.DAL.Context;
namespace DataBinding.Model.DAL
{
    class DBAnimal : CachedData<Animal>
    {
        public DBAnimal(AnimalContext context)
            :base(context)
        {            
        }
    }
}
