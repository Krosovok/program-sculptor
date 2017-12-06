using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.SqlFactory
{
    /// <summary>
    /// Manages the primary database connection strings and SQL strings in the web.config.
    /// Can be called without specifying a key, allowing reflection to select the proper SQL string, based on the calling class and function name.
    /// </summary>
    public static class SqlFactory
    {
        private const string TagName = "SqlFactory";
        private static readonly SqlFactorySectionHandler SqlStringFactoryConfiguration = 
            (SqlFactorySectionHandler)ConfigurationManager.GetSection(TagName);

        /// <summary>
        /// Default connection string used to connect to the primary database.
        /// </summary>
        public static string ConnectionString => ConfigurationManager
            .ConnectionStrings[SqlStringFactoryConfiguration.ConnectionStringName]
            .ConnectionString;
        
        public static string GetSql(string name, params string[] parameters)
        {
            if (name.Length == 0)
            {
                throw new ArgumentException("Empty Name given to SqlStringFactory.");
            }

            if (parameters.Length != SqlStringFactoryConfiguration.SqlStrings[name].ParametersCount)
            {
                throw new ArgumentException("Wrong number of argumetns given.");
            }

            return InsertParameters(
                SqlStringFactoryConfiguration.SqlStrings[name].Sql, 
                parameters);
        }
        
        private static string InsertParameters(string rawSql, params string[] parameters)
        {
            string readyQuery = string.Format(rawSql, parameters);
            // TODO: Check if format works right.
            
            return readyQuery;
            /*
            int parameterIndex = 0;
            foreach (string parameter in parameterList)
            {
                string parameterKey = "{" + parameterIndex + "}";

                if (rawSql.IndexOf(parameterKey, StringComparison.Ordinal) == -1)
                {
                    throw new Exception("Too many parameters passed into GetSql. Name: " + name + ", SQL: " + rawSql + ", Parameter Count: " + parameterList.Count);
                }

                // Replace the occurance of {0} with parameterList[0].
                rawSql = rawSql.Replace(parameterKey, parameter);

                parameterIndex++;
            }

            if (rawSql.IndexOf("{", StringComparison.Ordinal) != -1)
            {
                throw new Exception("Too few parameters passed into GetSql. Name: " + name + ", SQL: " + rawSql + ", Parameter Count: " + parameterList.Count);
            }
*/
        }
    }
}
