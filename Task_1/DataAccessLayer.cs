using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Task_1
{
    abstract class DataAccess<T>
    {                
        private string _connectionString;
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Connection string cannot be null or empty");
                }
                _connectionString = value;
            }
        }
        /// <summary>
        /// Открывает соединение с БД
        /// </summary>
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }
            return connection;
        }
        /// <summary>
        /// Закрывает соединение с БД
        /// </summary>
        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed) {
                connection.Close();
            }
        }
        /// <summary>
        /// Возвращает коллекцию типа <see cref="T"/>, наполненную объектами из БД
        /// </summary>        
        /// <param name="sql"></param>        
        public List<T> Load(string sql)
        {
            var connection = OpenConnection();
            var data = new List<T>();
            try {
                SqlCommand command = new SqlCommand(sql, connection);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var element = Serialize(reader, typeof(T));
                        data.Add(element);
                    }
                }
            }
            catch (SqlException) {
                throw;
            }
            catch (Exception) {
                throw;
            }
            finally {
                CloseConnection(connection);
            }            
            return data;
        }
        protected virtual T Serialize(SqlDataReader reader, Type type)
        {
            var element = InitFromRecordByType(type, reader);
            return element;
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
                if (property != null) {
                    property.SetValue(element, reader[i]);
                }
            }
            return (T) element;
        }
    }
}