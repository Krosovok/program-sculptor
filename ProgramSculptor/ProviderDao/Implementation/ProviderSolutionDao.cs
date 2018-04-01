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

        private ProviderSolutionDao() { }

        internal static ISolutionDao Instance => instance ?? (instance = new ProviderSolutionDao());

        public IReadOnlyList<Solution> GetUserTaskSolutions(Task task, string username)
        {
            task = NullIfDefault(task, Task.Sandbox);
            username = NullIfDefault(username, ProviderUsersDao.GuestUser);

            return new SolutionReader(task, username).GetList();
        }

        public IReadOnlyList<Solution> GetOthersSolutions(Task task, string username)
        {
            if (username == null)
                throw new ArgumentNullException(
                    nameof(username),
                    "For other's solutions username of the current user must be specified.");

            task = NullIfDefault(task, Task.Sandbox);

            SolutionReader reader = new SolutionReader(task, username) {OthersSolutions = true};
            return reader.GetList();
        }

        public Solution GetBaseSolution(Solution solution)
        {
            int? baseId = solution.BaseSolution;
            if (baseId == null)
            {
                return null;
            }
            
            SolutionReader reader = new SolutionReader();

            return reader.GetById((int) baseId);
        }

        public void AddSolution(Solution newSolution)
        {
            if (newSolution == null)
            {
                throw new ArgumentNullException(nameof(newSolution), "No solution given.");
            }
            if (newSolution.Id != Solution.NoId)
            {
                throw new ArgumentException("Solution already added.", nameof(newSolution));
            }
            
            newSolution.Id = InsertProcedure(newSolution);
        }

        public void DeleteSolution(Solution solutionToDelete)
        {
            if (solutionToDelete == null)
            {
                throw new ArgumentNullException(nameof(solutionToDelete), "No solution given.");
            }

            DeleteProcedure(solutionToDelete);
        }

        private static int InsertProcedure(Solution newSolution)
        {
            Db db = Db.Instance;
            DbCommand insertProcedure = db.CreatePrecedureCommand(InsertKey);

            DbParameter outputId = db.CreateOutputParameter(DbType.Int32, "NEW_SOLUTION_ID");
            Task solutionTask = NullIfDefault(newSolution.Task, Task.Sandbox);
            string solutionUser = NullIfDefault(newSolution.User, ProviderUsersDao.GuestUser);

            insertProcedure.Parameters.AddRange(new DbParameter[]
            {
                db.CreateParameter(newSolution.Name, DbType.String, "NEW_SOLUTION_NAME"),
                db.CreateParameter(solutionUser, DbType.String, "USER_NAME"),
                db.CreateParameter(solutionTask?.Id, DbType.Int32, "SOLVED_TASK_ID"),
                db.CreateParameter(newSolution.BaseSolution, DbType.Int32, "NEW_BASE_SOLUTION_ID"),
                outputId
            });
            
            db.TryExecuteNonQuery(insertProcedure);
            return (int) outputId.Value;
        }

        private static void DeleteProcedure(Solution solutionToDelete)
        {
            Db db = Db.Instance;
            string paramName = db.Param(Db.Solutions.Id);
            DbCommand delete = db.CreateTextCommand(DeleteKey, paramName);

            DbParameter idToDelete = db.CreateParameter(
                solutionToDelete.Id, DbType.Int32, paramName);
            delete.Parameters.Add(idToDelete);
            
            db.TryExecuteNonQuery(delete);
        }
        
        private static T NullIfDefault<T>(T value, T defaultValue) where T : class
        {
            return defaultValue.Equals(value) ? null : value;
        } 
    }
}