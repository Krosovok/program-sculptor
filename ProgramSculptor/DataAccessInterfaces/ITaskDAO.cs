using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Data access interface for tasks.
    /// </summary>
    public interface ITaskDao
    {
        /// <summary>
        /// Get all available tasks.
        /// </summary>
        /// <exception cref="DataAccessException">
        /// Occures when data can't be obtained because of error in data container. 
        /// Its message contains message that cound be displayed to user.
        /// </exception>
        IReadOnlyList<Task> AllTasks { get; }
    }
}
