using System;
using System.Data.Entity;
using System.Data.SqlClient;

using Task_1.Core;
using Task_1.DAL.Context;

namespace Task_1
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
