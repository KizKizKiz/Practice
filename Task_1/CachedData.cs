using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using Task_1.Core;
namespace Task_1
{
    abstract class CachedData<T> : DataAccess<T> where T : IKey
    {
        /// <summary>
        /// Имя таблицы, из которой происходит выборка данных
        /// </summary>
        public abstract string Table { get; }
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
        protected override T Serialize(SqlDataReader reader)
        {
            T data = default(T);
            switch ((SQUAD) reader["Squad"]) {
                case SQUAD.spiders: {
                    data = InitFromRecordByType(typeof(Spider), reader);
                    break;
                }
                case SQUAD.lepidoptera: {
                    data = InitFromRecordByType(typeof(Butterfly), reader);
                    break;
                }
                default:
                break;
            }
            return data;
        }
        /// <summary>
        /// Создает объект динамически и инициализирует свойства объекта из записи
        /// </summary>
        /// <param name="type">Тип объекта</param>
        /// <param name="reader">Объект, представляющий запись</param>
        /// <returns></returns>
        private T InitFromRecordByType(Type type, SqlDataReader reader)
        {
            var element = Activator.CreateInstance(type);

            var properties = type.GetProperties();
            var propertiesName = properties.Select((prop) => prop.Name).ToList();
            
            for (int i = 0; i < reader.FieldCount; i++) {
                var column = reader.GetName(i);
                var property = properties.FirstOrDefault((prop) => prop.Name == column);
                if (property!=null) {
                    property.SetValue(element, reader[i]);
                }                
            }
            return (T)element; 
        }

    }
}
