using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Task_1
{
    abstract class CachedData<T> : DataAccess<T> where T : IKey
    {
        /// <summary>
        /// Кэшированные данные
        /// </summary>
        protected Dictionary<int, T> _cachedData;
        /// <summary>
        /// Возвращает кэшированный элемент типа <see cref="T"/> из таблицы. Если элемент на найден
        /// в коллекции, он загружается из БД и кэшируется
        /// </summary>        
        /// <param name="id">Идентификатор объекта</param>
        /// <param name="table">Наименование таблицы</param>
        public T LoadById(int id, string table)
        {
            T element = _cachedData.Keys.Contains(id) ?
                        _cachedData[id] : default(T);                                    
            if (element == null) {
                element = Load($"SELECT * FROM {table} WHERE ID={id}").
                          FirstOrDefault();
                if (element != null) {
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
        public IEnumerable<T> LoadFromCacheByLinq(Predicate<T> match)
        {
            if (match == null) {
                throw new NullReferenceException();
            }
            return _cachedData.Values.
                               ToList().
                               FindAll(match);
        }
        /// <summary>
        /// Инициализация кэшированных данных типа <see cref="T"/>
        /// </summary>
        /// <param name="collection">Коллекция объектов инициализации</param>
        protected override void InitCacheData(IEnumerable<T> collection)
        {
            _cachedData = new Dictionary<int, T>();
            foreach (var element in collection) {                
                _cachedData.Add(element.Id, element);
            }
        }
        protected abstract override T Serialize(SqlDataReader reader);
    }
}
