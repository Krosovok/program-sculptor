using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.SqlFactory;

namespace ProviderDao
{
    internal class Db
    {
        /*private const string DEFAULT_DB_PROVIDER = "Oracle.DataAccess.Client";
        private const string DEFAULT_CONNECTION_STRING =
            "DATA SOURCE=PEN-PEN:1521/xe;PERSIST SECURITY INFO=True;USER ID=CSHARP;PASSWORD=CorporationOfEv1l";*/

        private static Db instance;
        private string dbProvider;

        private Db()
        {
            //DbProvider = DEFAULT_DB_PROVIDER;
        }

        public string DbProvider
        {
            get { return dbProvider; }
            set
            {
                dbProvider = value;
                Factory = DbProviderFactories.GetFactory(dbProvider);
            }
        }
        public string ConnectionString { get; set; }
        internal DbProviderFactory Factory { get; private set; }

        public static Db Instance => instance ?? (instance = new Db());

        internal DbCommand CreateTextCommand(string sqlKey)
        {
            DbCommand command = CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = SqlFactory.GetSql(sqlKey);
            return command;
        }

        internal DbParameter CreateParameter(object value, DbType type)
        {
            DbParameter parameter = Factory.CreateParameter();
            parameter.Value = value;
            parameter.DbType = type;
            
            //TODO: Something else?

            return parameter;
        }

        internal void CloseCommand(DbCommand command)
        {
            command.Connection.Close();
            command.Dispose();
        }

        private DbCommand CreateCommand()
        {
            DbCommand command = Factory.CreateCommand();
            command.Connection = CreateConnection();
            return command;
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }

        public static class Tasks
        {
            public const string Id = "task_id";
            public const string Name = "task_name";
            public const string Description = "description";
            public const string ChainId = "chain_id";
            public const string ChainPosition = "chain_position";
        }

        public static class Solutions
        {
            public const string Id = "solution_id";
            public const string Name = "solution_name";
            public const string BaseId = "base_solution_id";
        }

        public static class Users
        {
            public const string Id = "user_id";
            public const string Name = "username";
        }
    }
}