using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace WebApiTest.Providers
{
    public class DbSettingsSource<T> : FileConfigurationSource where T: IDbConnection, new()
    {
        public string Connection { get; }
        public string Query { get; }
        public DbSettingsSource(string connection, string query): this(connection, query, true) { }
        public DbSettingsSource(string connection, string query, bool optional)
        {
            Optional = optional;
            Connection = connection;
            Query = query;
        }
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbSettingsProvider<T>(this);
        }
    }
}
