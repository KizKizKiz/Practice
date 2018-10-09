using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

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
        }
        public IEnumerable<T> Load()
        {
            return Entity.ToList();
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
        public void Replace(T storage, T value)
        {
            if (!Entity.Local.Contains(storage)) {
                throw new ArgumentException("DbSet doesn't contain storage-value");
            }
            Remove(storage);
            Add(value);
            Context.Entry(value).State = EntityState.Modified;
        }
        public void Unchange(T item)
        {
            Context.Entry(item).State = EntityState.Unchanged;
        }
        public void Remove(T item)
        {
            Entity.Remove(item);            
        }
        public void Add(T item)
        {
            Entity.Add(item);
            //Context.Entry(item).State = EntityState.Added;
        }
        public T Save(T item)
        {      
            Context.SaveChanges();                              
            return item;
        }
        public bool HasChanged(T item)
        {
            Debug.WriteLine($"{item}\n{Context.Entry(item).State}");            
            return Context.Entry(item).State== EntityState.Modified;
        }
    }
}