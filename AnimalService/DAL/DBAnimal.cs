using System.Data.SqlClient;
using Practice.Core;
using System.ServiceModel;
using Service.DAL.Context;

namespace Service.DAL
{             
    public class DBAnimal : CachedData<Animal>
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
