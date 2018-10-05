using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Diagnostics;

namespace DataBinding.Model
{
    abstract class DataAccessLayer<T> where T : class
    {
        private protected DbContext _context;
        protected abstract DbContext Context { get; }
        protected DbSet<T> Entity { get; set; }
        public DataAccessLayer()
        {
            Entity = Context.Set<T>();
            Context.Configuration.AutoDetectChangesEnabled = false;
        }
        public IEnumerable<T> Load()
        {
            return Entity.AsNoTracking();
        }
        public IEnumerable<T> LoadWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Entity.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public void Attach(T item)
        {
            Context.Entry(item).State = EntityState.Unchanged;
        }
        public T Save(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
            Context.Entry(item).State = EntityState.Detached;
            return item;
        }
        //public void Attach(T item)
        //{            
        //    Entity.Attach(item);
        //}
        public bool HasChanged(T item)
        {
            Debug.WriteLine($"{item}\n{Context.Entry(item).State}");
            Context.ChangeTracker.DetectChanges();                        
            return Context.ChangeTracker.HasChanges();
        }
    }
}