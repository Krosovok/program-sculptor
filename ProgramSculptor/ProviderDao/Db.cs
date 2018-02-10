using System.Data;
using System.Data.Common;
using DB.SqlFactory;

namespace ProviderDao
{
    internal class Db
    {
        private static Db instance;
        
        private string dbProvider;
        private string parameterPrefix;

        private Db()
        {
            ConnectionString = SqlStringFactory.ConnectionString;
            parameterPrefix = SqlStringFactory.ParameterPrefix;
            DbProvider = SqlStringFactory.Provider;
        }

        private string ConnectionString { get; }

        private string DbProvider
        {
            set
            {
                dbProvider = value;
                Factory = DbProviderFactories.GetFactory(dbProvider);
            }
        }

        private DbProviderFactory Factory { get; set; }

        public static Db Instance => instance ?? (instance = new Db());

        internal string Param(string paramName) => parameterPrefix + paramName;

        internal DbCommand CreateTextCommand(string sqlKey, params string[] parameterPlaceholders)
        {
            DbCommand command = CreateCommand(sqlKey, parameterPlaceholders);
            command.CommandType = CommandType.Text;
            return command;
        }

        internal DbCommand CreatePrecedureCommand(string sqlKey)
        {
            DbCommand command = CreateCommand(sqlKey);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        internal DbParameter CreateParameter(object value, DbType type, string name)
        {
            DbParameter parameter = CreateParameter(type, name);
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;

            return parameter;
        }

        internal DbParameter CreateOutputParameter(DbType type, string name)
        {
            DbParameter parameter = CreateParameter(type, name);
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        internal DbParameter CreateReturnParameter(DbType type, string name)
        {
            DbParameter parameter = CreateParameter(type, name);
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.Size = 20;

            return parameter;
        }

        private DbParameter CreateParameter(DbType type, string name)
        {
            DbParameter parameter = Factory.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = name;
            return parameter;
        }

        internal void CloseCommand(DbCommand command)
        {
            command.Connection.Close();
            command.Dispose();
        }

        private DbCommand CreateCommand(string sqlKey, params string[] parameterPlaceholders)
        {
            DbCommand command = Factory.CreateCommand();
            command.Connection = CreateConnection();
            command.CommandText = SqlStringFactory.GetSql(sqlKey, parameterPlaceholders);
            return command;
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
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

        public static class CodeFiles
        {
            public const string Name = "file_path";
        }
    }
}