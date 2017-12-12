using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DB.SqlFactory
{
    public static class SqlStringFactory
    {
        private const string TagName = "SqlFactory";

        private static readonly SqlFactorySectionHandler SqlStringFactoryConfiguration =
            (SqlFactorySectionHandler) ConfigurationManager.GetSection(TagName);
        
        public static string ConnectionString => ConnectionStringSettings.ConnectionString;

        public static string Provider => ConnectionStringSettings.ProviderName;
        
        private static ConnectionStringSettings ConnectionStringSettings
        {
            get
            {
                ConnectionStringSettings connectionStringSettings = ConfigurationManager
                    .ConnectionStrings[SqlStringFactoryConfiguration.ConnectionStringName];
                return connectionStringSettings;
            }
        }
        
        public static string GetSql(string name, params string[] parameterPlaceholders)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Empty Name given to SqlStringFactory.");
            }

            int parametersCount = SqlStringFactoryConfiguration.SqlStrings[name].ParametersCount;
            if (parameterPlaceholders.Length != parametersCount)
            {
                throw new ArgumentException($"Wrong number of parameters for {name}: {parametersCount}");
            }
            
            return FormatParameters(name, parameterPlaceholders);
        }

        private static string FormatParameters(string name, params string[] parameterPlaceholders)
        {
            string rawSql = SqlStringFactoryConfiguration.SqlStrings[name].Sql;
            return string.Format(rawSql, parameterPlaceholders);
        }
    }
}