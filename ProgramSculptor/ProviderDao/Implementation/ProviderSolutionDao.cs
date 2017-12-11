using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using Model;

namespace ProviderDao.Implementation
{
    internal class ProviderSolutionDao : ISolutionDao
    {
        private const string InsertKey = "ProviderSolutionDao.AddSolution";
        private const string DeleteKey = "ProviderSolutionDao.DeleteSolution";

        public IReadOnlyList<Solution> GetMyTaskSolutions(Task task, string username)
        {
            return new SolutionReader(task, username).GetList();
        }

        public void AddSolution(Solution newSolution)
        {
            DbCommand insertProcedure = Db.Instance.CreatePrecedureCommand(InsertKey);

            insertProcedure.Parameters.AddRange(new DbParameter[]
            {
                Db.Instance.CreateParameter(newSolution.Name, DbType.String, "NEW_SOLUTION_NAME"),
                Db.Instance.CreateParameter(newSolution.User, DbType.String, "USER_NAME"),
                Db.Instance.CreateParameter(newSolution.Task.Id, DbType.Int32, "SOLVED_TASK_ID"),
                Db.Instance.CreateParameter(newSolution.BaseSolution, DbType.Int32, "NEW_BASE_SOLUTION_ID")
            });

            insertProcedure.ExecuteNonQuery();
            
            // TODO: Add id to solution.

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
    }
}