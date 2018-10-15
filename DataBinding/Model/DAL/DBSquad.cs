using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Model.DAL
{
    class DBSquad:CachedData<AnimalType>
    {
        public DBSquad(DbContext context)
            :base(context)
        {
        }
    }
}
