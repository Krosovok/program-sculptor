﻿using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface ISolutionDao
    {
        IReadOnlyList<Solution> GetMyTaskSolutions(Task task, string username);

        void AddSolution(Solution newSolution); // Procedure for this.

        void DeleteSolution(Solution solutionToDelete);
    }
}
