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
        private SqlConnection OpenConnection(string strConn)
        {
            SqlConnection connection = new SqlConnection(strConn);
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
        public List<T> Load(string sql, string strConn)
        {
            var connection = OpenConnection(strConn);
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
            return data;
        }

        protected abstract T Serialize(SqlDataReader reader);        
    }
}
