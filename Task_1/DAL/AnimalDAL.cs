using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Task_1.Core;
namespace Task_1.DAL
{
    class AnimalDAL<T> where T:Animal,new()
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
        /// Возвращает коллекцию типа <see cref="T"/>, наполненную объектами из БД
        /// </summary>
        /// <param name="sql"></param>        
        public List<T> Load(string sql)
        {
            var animals = new List<T>();
            _command.CommandText = sql;
            using (var reader = _command.ExecuteReader()) {
                while (reader.Read()) {
                    var animal = new T();
                    animal.Serialize(reader);
                    animals.Add(animal);
                }
            }
            return animals;
        }
        /// <summary>
        /// Закрывает соединение с БД
        /// </summary>
        public void CloseConnection()
        {
            if (_connection.State!=ConnectionState.Closed) {
                _connection.Close();
            }
        }
    }
}
