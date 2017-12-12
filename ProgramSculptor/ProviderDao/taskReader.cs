using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DB.SqlFactory;
using Model;

namespace ProviderDao
{
    internal class TaskReader : DbReader<Task>
    {
        private const string SqlKey = "TaskReader.AllTasks";
        
        private readonly Dictionary<int, Dictionary<int, Task>> chains = new Dictionary<int, Dictionary<int, Task>>();

        public override List<Task> GetList()
        {
            base.GetList();

            foreach (var chain in chains)
            {
                TaskChain.RegisterInTaskChain(chain.Value.Values);
            }

            return Data;
        }

        protected override DbCommand SelectCommand()
        {
            return Db.Instance.CreateTextCommand(SqlKey);
        }

        protected override Task ReadRow(IDataRecord data)
        {
            Task task = BuildTask(data);

            AddToChain(data, task);

            return task;
        }

        private static Task BuildTask(IDataRecord data)
        {
            int id = GetInt32NotNull(data, Db.Tasks.Id);
            string name = GetString(data, Db.Tasks.Name);
            string description = GetString(data, Db.Tasks.Description);

            return new Task(id, name, description);
        }

        private void AddToChain(IDataRecord data, Task task)
        {
            int? chainId = GetInt32(data, Db.Tasks.ChainId);
            int? chainPosition = GetInt32(data, Db.Tasks.ChainPosition);
            if (chainId == null || chainPosition == null)
            {
                return;
            }

            if (!chains.ContainsKey((int) chainId))
            {
                chains[(int) chainId] = new Dictionary<int, Task>();
            }
            chains[(int) chainId].Add((int) chainPosition, task);
        }
    }
}