using System.Data;
using System.Data.Common;
using Model;

namespace ProviderDao
{
    internal class SolutionReader : DbReader<Solution>
    {
        private readonly Task task;
        private readonly string username;
        
        private const string SqlKey = "SolutionReader.MySolutions";

        internal SolutionReader(Task task, string username)
        {
            this.task = task;
            this.username = username;
        }

        protected override DbCommand SelectCommand()
        {
            Db db = Db.Instance;
            string[] parameters = { db.Param(Db.Tasks.Id), db.Param(Db.Users.Name) };
            
            DbCommand select = db.CreateTextCommand(SqlKey, parameters);

            DbParameter taskId = db.CreateParameter(task?.Id, DbType.Int32, parameters[0]);
            DbParameter userName = db.CreateParameter(username, DbType.String, parameters[1]);
            select.Parameters.AddRange(new[]
            {
                taskId, userName
            });
            return select;
        }
        
        protected override Solution ReadRow(IDataRecord data)
        {
            int id = GetInt32NotNull(data, Db.Solutions.Id);
            string solutionName = GetString(data, Db.Solutions.Name);
            int? baseId = GetInt32(data, Db.Solutions.BaseId);
            
            return new Solution(id, solutionName, username, task, baseId);
        }

    }
}