using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Task_1.Core;
namespace Task_1
{
    abstract class DataAccess<T>
    {        
        /// <summary>
        /// Открывает соединение с БД
        /// </summary>
        public SqlConnection OpenConnection(string strConn)
        {
            SqlConnection _connection = new SqlConnection(strConn);
            SqlCommand _command = new SqlCommand(null, _connection);
            _connection.Open();
            return _connection;
        }
        /// <summary>
        /// Закрывает соединение с БД
        /// </summary>
        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed) {
                connection.Close();
            }
        }
        /// <summary>
        /// Возвращает коллекцию типа <see cref="T"/>, наполненную объектами из БД
        /// </summary>
        /// <param name="sql"></param>        
        public List<T> Load(string sql, SqlConnection connection)
        {
            var data = new List<T>();
            SqlCommand command = new SqlCommand(sql, connection);            
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {     
                    
                    var element = Serialize(reader);
                    data.Add(element);
                }
            }
            return data;
        }

        protected abstract T Serialize(SqlDataReader reader);        
    }
}
