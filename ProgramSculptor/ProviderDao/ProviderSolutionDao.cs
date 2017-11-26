using System;
using System.Collections.Generic;
using DataAccessInterfaces;
using Model;

namespace ProviderDao
{
    class ProviderSolutionDao : ISolutionDao
    {
        private List<Solution> solutions = new List<Solution>();
        
        // TODO: Some way to retrieve inteface instances in runtime. Aka Singleton. 
        
        public IReadOnlyList<Solution> GetTaskSolutions(Task task)
        {
            throw new NotImplementedException();
            // TODO: Finish.
        }

        public void AddSolution(Solution newSolution)
        {
            throw new NotImplementedException();
            // TODO: Finish.
        }

        public void DeleteSolution(Solution solutionToDelete)
        {
            throw new NotImplementedException();
            // TODO: Finish.
        }
    }
}
