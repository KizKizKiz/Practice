using System;
using System.Data.Entity;
using System.Data.SqlClient;
using DataBinding.Core;
using DataBinding.Model.DAL.Context;
namespace DataBinding.Model.DAL
{
    class DBAnimal : CachedData<Animal>
    {
        protected override DbContext Context
        {
            get
            {
                if (_context==null) {
                    _context = new AnimalContext();
                }
                return _context;
            }
        }
    }
}
