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
        /// <summary>
        /// Возвращает список объектов тип <see cref="T"/> загруженных из базы данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Load()
        {
            return Entity;            
        }      
        /// <summary>
        /// Возвращает объект типа <see cref="T"/> из базы данных в неизменном состоянии
        /// </summary>
        /// <param name="item">Требуемый аргумент</param>        
        public T Reload(T item)
        {
            Context.Entry(item).Reload();            
            return item;
        }
        /// <summary>
        /// Сохраняет объект в базе данных. 
        /// В случае не нулевого значения <paramref name="id"/> произойдет поиск
        /// объекта. Если искомый объект НЕ <see langword="null"/> 
        /// он удалится и добавится <paramref name="item"/>,
        /// иначе будет добавлен новый <paramref name="item"/>
        /// </summary>
        /// <param name="item">Сохраняемый объект</param>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Сохраненный объект</returns>
        public T Save(T item, int id=0)
        {
            if (Context.Entry(item).State== EntityState.Modified) {
                Context.SaveChanges();
                return item;
            }
            var elementById = Entity.Find(id);            
            if (elementById != null) {                
                Entity.Remove(elementById);
                Entity.Add(item);
                Context.SaveChanges();
                return item;
            }
            Context.Entry(item).State = EntityState.Added;
            Context.SaveChanges();
            return item;
        }
        /// <summary>
        /// Возвращает <see langword="true"/> если объект изменен или не отслеживается контекстом,
        /// иначе <see langword="false"/>
        /// </summary>
        /// <param name="item">Проверяемый объект</param>
        
        public bool HasModifiedOrDetached(T item)
        {            
            return Context.Entry(item).State == EntityState.Detached ||
                Context.Entry(item).State == EntityState.Modified;
        }
    }
}