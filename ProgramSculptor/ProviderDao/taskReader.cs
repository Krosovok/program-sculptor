using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DB.SqlFactory;
using Model;

namespace ProviderDao
{
    public class TaskReader : DbReader
    {
        private const string SqlKey = "TaskReader.AllTasks";
        private List<Task> tasks;
        private Dictionary<int, List<Task>> chains = new Dictionary<int, List<Task>>();

        public List<Task> TaskList()
        {
            ReadData();
            
            foreach (var chain in chains)
            {
                TaskChain.RegisterInTaskChain(chain.Value);
            }

            return tasks;
        }

        private void ReadData()
        {
            DbCommand select = SelectCommand();

            tasks = new List<Task>();
            ReadAllRows(@select);

            Db.Instance.CloseCommand(@select);
        }

        private static DbCommand SelectCommand()
        {
            DbCommand select = Db.Instance.CreateCommand();
            select.CommandType = CommandType.Text;
            select.CommandText = SqlFactory.GetSql(SqlKey);
            return select;
        }

        private void ReadAllRows(DbCommand select)
        {
            using (DbDataReader reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    Task item = ReadTask(reader);
                    tasks.Add(item);
                }
            }
        }

        private Task ReadTask(IDataRecord data)
        {
            Task task = BuildTask(data);
            
            AddToChain(data, task);

            return task;
        }

        private void AddToChain(IDataRecord data, Task task)
        {
            int? chainId = GetInt32(data, Db.Tasks.ChainId);
            int? chainPosition = GetInt32(data, Db.Tasks.ChainPosition);
            if (chainId == null || chainPosition == null)
            {
                return;
            }
            
            if (!chains.ContainsKey((int)chainId))
            {
                chains[(int)chainId] = new List<Task>();
            }
            chains[(int)chainId].Insert((int)chainPosition, task);
        }

        private static Task BuildTask(IDataRecord data)
        {
            int id = GetInt32NotNull(data, Db.Tasks.Id);
            string name = GetString(data, Db.Tasks.Name);
            string description = GetString(data, Db.Tasks.Description);

            return new Task(id, name, description);
        }
    }
}