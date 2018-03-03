using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using Model;

namespace ViewModel.Core
{
    public class AllTasks
    {
        // TODO: Refactor TaskList using this.
        
        public AllTasks()
        {
            IReadOnlyList<Task> tasks = Dao.Factory.TaskDao.AllTasks;
            Tasks = tasks.Select(task => new TaskSolutions(task));
        }
        
        public IEnumerable<TaskSolutions> Tasks { get; }
    }
}
