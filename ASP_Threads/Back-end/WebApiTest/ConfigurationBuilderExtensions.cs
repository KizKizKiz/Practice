using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApiTest.Providers;

namespace WebApiTest
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDbSettings<T>(this IConfigurationBuilder builder, string connection, string query)
            where T : IDbConnection, new()
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connection) || string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(connection == null ? nameof(connection) : nameof(query));

            var dbSource = new DbSettingsSource<T>(connection, query);
            builder.Sources.Insert(0, dbSource);

            return builder;
        }
        public static IConfigurationBuilder AddDbSettings<T>(this IConfigurationBuilder builder, string connection, string query, bool optional)
            where T : IDbConnection, new()
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(connection) || string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(connection == null ? nameof(connection) : nameof(query));

            var dbSource = new DbSettingsSource<T>(connection, query)
            {
                Optional = optional
            };
            builder.Sources.Insert(0, dbSource);

            return builder;
        }
    }
}
