using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using Model;

namespace ProviderDao.Implementation
{
    internal class ProviderSolutionDao : ISolutionDao
    {
        // TODO: Check new tasks for name "Sandbox" and users for "Guest", as they are reserved.

        private const string InsertKey = "ProviderSolutionDao.AddSolution";
        private const string DeleteKey = "ProviderSolutionDao.DeleteSolution";
        private static ISolutionDao instance;

        private ProviderSolutionDao()
        {
        }

        internal static ISolutionDao Instance => instance ?? (instance = new ProviderSolutionDao());

        public IReadOnlyList<Solution> GetUserTaskSolutions(Task task, string username)
        {
            task = NullIfDefault(task, Task.Sandbox);
            username = NullIfDefault(username, ProviderUsersDao.GuestUser);

            return new SolutionReader(task, username).GetList();
        }

        public void AddSolution(Solution newSolution)
        {
            DbCommand insertProcedure = Db.Instance.CreatePrecedureCommand(InsertKey);

            DbParameter outputId = Db.Instance.CreateOutputParameter(DbType.Int32, "NEW_SOLUTION_ID");
            Task solutionTask = NullIfDefault(newSolution.Task, Task.Sandbox);
            string solutionUser = NullIfDefault(newSolution.User, ProviderUsersDao.GuestUser);
            
            insertProcedure.Parameters.AddRange(new DbParameter[]
            {
                Db.Instance.CreateParameter(newSolution.Name, DbType.String, "NEW_SOLUTION_NAME"),
                Db.Instance.CreateParameter(solutionUser, DbType.String, "USER_NAME"),
                Db.Instance.CreateParameter(solutionTask?.Id, DbType.Int32, "SOLVED_TASK_ID"),
                Db.Instance.CreateParameter(newSolution.BaseSolution, DbType.Int32, "NEW_BASE_SOLUTION_ID"),
                outputId
            });

            insertProcedure.ExecuteNonQuery();

            newSolution.Id = (int) outputId.Value;

            Db.Instance.CloseCommand(insertProcedure);
        }

        public void DeleteSolution(Solution solutionToDelete)
        {
            Db db = Db.Instance;
            string paramName = db.Param(Db.Solutions.Id);
            DbCommand delete = db.CreateTextCommand(DeleteKey, paramName);

            DbParameter idToDelete = db.CreateParameter(
                solutionToDelete.Id, DbType.Int32, paramName);
            delete.Parameters.Add(idToDelete);
            delete.ExecuteNonQuery();

            db.CloseCommand(delete);
        }
        
        private static T NullIfDefault<T>(T value, T defaultValue) where T : class
        {
            return defaultValue.Equals(value) ? null : value;
        } 
    }
}