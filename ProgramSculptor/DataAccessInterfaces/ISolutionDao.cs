using System;
using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Data access interface for accessing Solution objects and manipulating them.
    /// </summary>
    public interface ISolutionDao
    {
        /// <summary>
        /// Retrurns all Solutions of the given Task made by user with given username.
        /// </summary>
        /// <param name="task">Task object which solutions should be found. If null or Task.Sandbox, than Solutions of the Sandbox will be found.</param>
        /// <param name="username">Username of user, whose solutions should be found. If null or special "Guest" value, than Slutions not bound to user will be found.</param>
        /// <exception cref="DataAccessException">
        /// Occures when data can't be obtained because of error in data container. 
        /// Its message contains message that cound be displayed to user.
        /// </exception>
        /// <returns>List of all solutions of Task by user with given username.</returns>
        IReadOnlyList<Solution> GetUserTaskSolutions(Task task, string username);

        /// <summary>
        /// Returns all solutions of the given task made by non-guest users with username different from given.
        /// </summary>
        /// <param name="task">Task object which solutions should be found. If null or Task.Sandbox, than Solutions of the Sandbox will be found.</param>
        /// <param name="username">Username that will be ignored in search. Null value is not accepted.</param>        
        /// <exception cref="DataAccessException">
        /// Occures when data can't be obtained because of error in data container. 
        /// Its message contains message that cound be displayed to user.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Given username is null.
        /// </exception>
        /// <returns>All solutions of other users. Solution objects doesn't have to be connected with local solutions, their id is id in global data storage.</returns>
        IReadOnlyList<Solution> GetOthersSolutions(Task task, string username);

        /// <summary>
        /// Adds a solution with given properties to the data storage. Assigns Id to the solution.
        /// </summary>        
        /// <exception cref="DataAccessException">
        /// Occures when data can't be obtained because of error in data container. 
        /// Its message contains message that cound be displayed to user.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Given solution is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Given solution already initialized.
        /// </exception>
        /// <param name="newSolution">Solution without Id to be added.</param>
        void AddSolution(Solution newSolution);

        /// <summary>
        /// Removes given solution from storage using id as identifier.
        /// </summary>
        /// <exception cref="DataAccessException">
        /// Occures when data can't be obtained because of error in data container. 
        /// Its message contains message that cound be displayed to user.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Given solution is null.
        /// </exception>
        /// <param name="solutionToDelete">Solution with id that identifies deleted Solution.</param>
        void DeleteSolution(Solution solutionToDelete);
    }
}
