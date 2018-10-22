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
            : base(context)
        {
        }
        public void DiscriminatorUpdate(Animal animal)
        {
            var sql = $"update Animals " +
                      $"set Discriminator=@discr " +
                      $"where Id=@id";
            Context.Database.ExecuteSqlCommand(sql,
                new SqlParameter[]
                {
                    new SqlParameter("@discr", animal.GetType().Name),
                    new SqlParameter("@id", animal.ID)
                });
        }
    }
}
