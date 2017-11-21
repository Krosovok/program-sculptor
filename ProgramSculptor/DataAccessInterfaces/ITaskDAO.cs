using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface ITaskDao
    {
        IReadOnlyList<Task> AllTasks { get; }
    }
}
