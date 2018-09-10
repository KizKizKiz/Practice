using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace Task_1
{
    abstract class DataAccess<T>
    {
        /// <summary>
        /// Кэшированные данные
        /// </summary>
        protected List<T> _cachedData;
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
                        var element = Serialize(reader);
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
            _cachedData = new List<T>(data);
            return data;
        }

        protected abstract T Serialize(SqlDataReader reader);
    }
}