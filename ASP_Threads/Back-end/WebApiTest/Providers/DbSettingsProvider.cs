using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace WebApiTest.Providers
{
    public class DbSettingsProvider<T> : ConfigurationProvider where T : IDbConnection, new()
    {
        public DbSettingsSource<T> DbSettingsSource { get; }
        public DbSettingsProvider(DbSettingsSource<T> dbSource)
        {
            DbSettingsSource = dbSource;
        }

        public override void Load()
        {
            using var connection = new T
            {
                ConnectionString = DbSettingsSource.Connection
            };
            connection.Open();
            using var command =  connection.CreateCommand();
            command.CommandText = DbSettingsSource.Query;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Data.Add(reader["Name"].ToString(), reader["Value"].ToString());
            }
        }
    }
}
