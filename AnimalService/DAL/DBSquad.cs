using System.Data.Entity;
using System.ServiceModel;

namespace Service.DAL
{    
    public class DBSquad:CachedData<AnimalType>
    {
        public DBSquad(DbContext context)
            :base(context)
        {
        }
        protected override string Discriminator => null;
        protected override string Table => "Squads";
    }
}
