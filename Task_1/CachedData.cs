using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;

namespace Task_1
{
    abstract class CachedData<T> : DataAccess<T> where T : IKey
    {
        protected string _table;
        /// <summary>
        /// Имя таблицы, из которой происходит выборка данных
        /// </summary>
        public abstract string Table { get; set; }        
        /// <summary>
        /// Кэшированные данные
        /// </summary>
        protected Dictionary<int, T> _cachedData = new Dictionary<int, T>();
        /// <summary>
        /// Возвращает кэшированный элемент типа <see cref="T"/> из таблицы. Если элемент на найден
        /// в коллекции, он загружается из БД и кэшируется
        /// </summary>        
        /// <param name="id">Идентификатор объекта</param>
        /// <param name="table">Наименование таблицы</param>
        public T LoadById(int id)
        {
            if (!_cachedData.TryGetValue(id, out T element)) {
                if (element == null) {
                    element = Load($"SELECT * FROM {Table} WHERE ID={id}").
                              FirstOrDefault();
                    _cachedData.Add(id, element);                    
                }
            }
            return element;
        }
        /// <summary>
        /// Возвращает коллекцию типа <see cref="T"/>, где каждый элемент удволетворяет предикату
        /// </summary>
        /// <param name="match">Условие отбора элементов</param>
        /// <returns></returns>
        public IEnumerable<T> LoadFromCacheByLinq(Func<T, bool> match)
        {
            if (match == null) {
                throw new NullReferenceException();
            }
            return _cachedData.Values.Where(match);                        
        }        
        protected abstract override T Serialize(SqlDataReader reader);
    }
}
