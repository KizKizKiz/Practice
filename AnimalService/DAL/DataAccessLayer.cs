using CommonLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Service.DAL
{
    public abstract class DataAccessLayer<T> where T : class
    {
        public DbContext Context { get; }
        private DbSet<T> Entity { get; set; }
        public DataAccessLayer(DbContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }
        /// <summary>
        /// Возвращает имя столбца дискриминатора в таблице <see cref="Table"/>
        /// </summary>
        protected abstract string Discriminator { get; }
        /// <summary>
        /// Возвращает имя таблицы привязки
        /// </summary>
        protected abstract string Table { get; }
        /// <summary>
        /// Возвращает коллекцию всех сущностей типа <see cref="T"/>, которые
        /// запрашиваются из таблицы базы данных. 
        /// При первом вызове сущности загружает всю таблицу в контекст данных.
        /// </summary>    
        public IQueryable<T> LazyLoadTable()
        {
            return Entity;
        }
        /// <summary>
        /// Возвращает объект типа <see cref="T"/> из <see cref="Table"/> в неизменном состоянии
        /// </summary>
        /// <param name="item">Требуемый аргумент</param>        
        public T Reload(T item)
        {
            Context.Entry(item).Reload();
            return item;
        }
        /// <summary>
        /// Сохраняет объект в таблице базы данных по идентификатору. 
        /// Если параметр <paramref name="id"/> имеет значение отличное от -1, будет осуществлен поиск элемента в кэше или при отсутствии - в <see cref="Table"/>.         
        /// </summary>
        /// <param name="item">Сохраняемый объект</param>        
        /// <param name="id">Уникальный идентификатор</param>
        public T Save(T item, int id)
        {
            try {
                var elementByKey = Entity.Find(id);
                if (elementByKey != null) {
                    Dettach(elementByKey);
                    Attach(item);
                    if (elementByKey.GetType() != item.GetType()) {
                        if (!string.IsNullOrWhiteSpace(Discriminator)) {
                            UpdateDiscriminator(item, id);
                        }
                    }
                }
                Context.SaveChanges();
            }
            catch (Exception exc) {
                throw new DbUpdateException("Cannot save, see inner exception", exc);
            }                                   
            return item;
        }
        private void UpdateDiscriminator(T item, int id)
        {
            var sql = $"update {Table} " +
                      $"set {Discriminator}=@discr " +
                      $"where Id=@id";
            Context.Database.ExecuteSqlCommand(sql,
                new SqlParameter[]
                {
                    new SqlParameter("@discr", item.GetType().Name),
                    new SqlParameter("@id", id)
                });
        }        
        /// <summary>
        /// Добавляет элемент для отслеживания контекстом данных и устанавливает состояние в неизменное.
        /// При изменении объекта метод SaveChanges() сгенерирует SQL запрос c изменениями объекта в базе данных
        /// </summary>        
        private void Attach(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }
        /// <summary>
        /// Удаляет элемент из отслеживания контекстом данных.
        /// При изменении неотслеживаемого объекта метод SaveChanges() НЕ будет генерировать SQL запрос с изменениями к базе данных
        /// </summary>        
        private void Dettach(T item)
        {
            Context.Entry(item).State = EntityState.Detached;
        }
    }
}