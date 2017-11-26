using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderDao
{
    public class Db
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

        internal DbCommand CreateCommand()
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

        public void CloseCommand(DbCommand command)
        {
            command.Connection.Close();
            command.Dispose();
        }

        public static class Tasks
        {
            public const string Id = "task_id";
            public const string Name = "task_name";
            public const string Description = "description";
            public const string ChainId = "chain_id";
            public const string ChainPosition = "chain_position";
        }
    }
}