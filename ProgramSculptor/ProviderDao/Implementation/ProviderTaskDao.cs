using System.Collections.Generic;
using DataAccessInterfaces;
using Model;

namespace ProviderDao.Implementation
{
    public class ProviderTaskDao : ITaskDao
    {
        private List<Task> tasks;

        public IReadOnlyList<Task> AllTasks
        {
            get
            {
                if (tasks == null)
                {
                    TaskReader reader = new TaskReader();
                    tasks = reader.GetList();
                }

                return tasks.AsReadOnly();
            }
        }
    }
}
