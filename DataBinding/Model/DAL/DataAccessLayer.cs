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
        public DbContext Context { get; }

        private DbSet<T> Entity { get; set; }
        public DataAccessLayer(DbContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }
        /// <summary>
        /// Возвращает коллекцию всех сущностей типа <see cref="T"/>, которые
        /// запрашиваются из таблицы базы данных. 
        /// При первом вызове сущности загружает всю таблицу в контекст данных.
        /// </summary>        
        public IEnumerable<T> LazyLoadTable()
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
        /// Сохраняет объект в таблице базы данных.                
        /// </summary>
        /// <param name="item">Сохраняемый объект</param>        
        /// <returns>Сохраненный объект</returns>
        public T Save(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
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
            Debug.WriteLine(Context.Entry(item).State);
            return Context.Entry(item).State == EntityState.Modified;
        }
        /// <summary>
        /// Добавляет элемент для отслеживания контекстом данных и устанавливает состояние в неизменное.
        /// При изменении объекта метод SaveChanges() сгенерирует SQL запрос c изменениями объекта в базе данных
        /// </summary>        
        public void Attach(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }
        /// <summary>
        /// Удаляет элемент из отслеживания контекстом данных.
        /// При изменении неотслеживаемого объекта метод SaveChanges() НЕ будет генерировать SQL запрос с изменениями к базе данных
        /// </summary>        
        public void Dettach(T item)
        {
            Context.Entry(item).State = EntityState.Detached;
        }
    }
}