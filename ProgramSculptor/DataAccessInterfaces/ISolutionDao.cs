using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface ISolutionDao
    {
        IReadOnlyList<Solution> GetTaskSolutions(Task task);

        void AddSolution(Solution newSolution); // Procedure for this.

        void DeleteSolution(Solution solutionToDelete);
    }
}
