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
        protected override string Discriminator => "Discriminator";
        protected override string Table => "Animals";
    }
}
