using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using DB.SqlFactory;
using Model;

namespace ProviderDao
{
    class ProviderSolutionDao : ISolutionDao
    {
        private const string SqlKey = "ProviderSolutionDao.DeleteSolution";
        
        // TODO: Some way to retrieve inteface instances in runtime. Aka Singleton. 
        
        public IReadOnlyList<Solution> GetMyTaskSolutions(Task task, string username)
        {
            return new SolutionReader(task, username).GetList();
        }

        public void AddSolution(Solution newSolution)
        {
            throw new NotImplementedException();
        }

        public void DeleteSolution(Solution solutionToDelete)
        {
            DbCommand delete = Db.Instance.CreateTextCommand(SqlKey);
        
            DbParameter idToDelete = Db.Instance.CreateParameter(
                solutionToDelete.Id, DbType.Int32);
            delete.Parameters.Add(idToDelete);
            delete.ExecuteNonQuery();

            Db.Instance.CloseCommand(delete);
        }
    }
}
