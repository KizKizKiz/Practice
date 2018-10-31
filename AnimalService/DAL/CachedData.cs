using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Practice.Core;
using System.ServiceModel;

namespace Service.DAL
{ 
    public abstract class CachedData<T> : DataAccessLayer<T> where T : class, IKey
    {
        public CachedData(DbContext context)
            :base(context)
        {
        }
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
                    element = LazyLoadTable().
                              FirstOrDefault(elem => elem.ID == id);
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
    }
}
