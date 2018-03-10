using System;
using System.Data;
using System.Data.Common;
using Model;
using ProviderDao.Implementation;

namespace ProviderDao
{
    internal class SolutionReader : DbReader<Solution>
    {
        private const string UserKey = "SolutionReader.MySolutions";
        private const string OthersKey = "SolutionReader.OthersSolutions";

        private readonly Task task;
        private readonly string username;

        internal SolutionReader(Task task, string username)
        {
            this.task = task;
            this.username = username;
        }

        /// <summary>
        /// If set to true, result will be solutions of users that are not same as given.
        /// If set to false, result will be solutions of given user only.
        /// Default - false.
        /// </summary>
        public bool OthersSolutions { get; set; }

        protected override DbCommand SelectCommand()
        {
            Db db = Db.Instance;
            string[] parameters = { db.Param(Db.Tasks.Id), db.Param(Db.Users.Name) };
            
            DbCommand select = db.CreateTextCommand(GetSqlKey(), parameters);
            
            object id = task?.Id ?? (object)DBNull.Value;
            object usernameParameter = username ?? (object)DBNull.Value;
            DbParameter taskId = db.CreateParameter(id, DbType.Int32, parameters[0]);
            DbParameter userName = db.CreateParameter(usernameParameter, DbType.String, parameters[1]);
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
            string solver = DefaultIfNull(username, ProviderUsersDao.GuestUser);
            Task solved = DefaultIfNull(task, Task.Sandbox);
            int? baseId = GetInt32(data, Db.Solutions.BaseId);
            
            return new Solution(id, solutionName, solver, solved, baseId);
        }

        private static T DefaultIfNull<T>(T value, T defaultValue)
        {
            return value == null ? defaultValue : value;
        }
        
        private string GetSqlKey()
        {
            return OthersSolutions ?
                OthersKey :
                UserKey;
        }
    }
}