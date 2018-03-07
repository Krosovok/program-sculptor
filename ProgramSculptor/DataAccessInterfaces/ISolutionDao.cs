using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface ISolutionDao
    {
        IReadOnlyList<Solution> GetUserTaskSolutions(Task task, string username);

        void AddSolution(Solution newSolution);

        void DeleteSolution(Solution solutionToDelete);
    }
}
