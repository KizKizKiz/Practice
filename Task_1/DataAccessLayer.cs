using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Task_1.Core;
namespace Task_1
{
    class DataAccess<T>
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        /// <summary>
        /// Открывает соединение с БД
        /// </summary>
        public void OpenConnection(string strConn)
        {
            _connection = new SqlConnection(strConn);
            _command = new SqlCommand(null, _connection);
            _connection.Open();
        }
        /// <summary>
        /// Закрывает соединение с БД
        /// </summary>
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed) {
                _connection.Close();
            }
        }
        /// <summary>
        /// Возвращает коллекцию типа <see cref="T"/>, наполненную объектами из БД
        /// </summary>
        /// <param name="sql"></param>        
        public List<T> Load(string sql)
        {
            var data = new List<T>();

            _command.CommandText = sql;
            using (var reader = _command.ExecuteReader()) {
                while (reader.Read()) {     
                    
                    var element = Serialize(reader);
                    data.Add(element);
                }
            }
            return data;
        }           

        protected virtual T Serialize(SqlDataReader reader)
        {
            return default(T);
        }
    }
}
