using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Task_1
{
    abstract class CachedData<T> : DataAccess<T> where T : IKey
    {
        /// <summary>
        /// Возвращает кэшированный элемент типа <see cref="T"/>. Если элемент на найден
        /// в коллекции, он загружается из БД и кэшируется
        /// </summary>        
        /// <param name="id">Идентификатор объекта</param>
        public T LoadById(int id)
        {
            T element = _cachedData.Where((data => data.Id == id)).
                                   FirstOrDefault();
            if (element == null) {
                element = Load($"SELECT * FROM ANIMALS WHERE ID={id}").
                          FirstOrDefault();
                if (element != null) {
                    _cachedData.Add(element);
                }
            }
            return element;
        }
        /// <summary>
        /// Возвращает коллекцию типа <see cref="T"/>, где каждый элемент удволетворяет предикату
        /// </summary>
        /// <param name="match">Условие отбора элементов</param>
        /// <returns></returns>
        public IEnumerable<T> LoadFromCacheByLinq(Predicate<T> match)
        {
            if (match == null) {
                throw new NullReferenceException();
            }
            return _cachedData.FindAll(match);
        }
        protected abstract override T Serialize(SqlDataReader reader);
    }
}
