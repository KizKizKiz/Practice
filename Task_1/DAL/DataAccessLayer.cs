using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Task_1
{
    abstract class DataAccessLayer<T>:IDisposable where T : class
    {
        private protected DbContext _context;
        protected abstract DbContext Context { get; }
        protected DbSet<T> Entity { get; set; }
        public DataAccessLayer()
        {
            Entity = Context.Set<T>();     
            
        }
        public IEnumerable<T> Load()
        {
            return Entity.AsNoTracking().ToList();
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
        public void Add(T item)
        {
            Context.Entry(item).State = EntityState.Added;
            Entity.Add(item);
        }
        public void Update()
        {
            foreach (var element in Entity) {
                Context.Entry(element).State = EntityState.Modified;
            }
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}