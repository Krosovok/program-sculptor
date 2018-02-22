using System.Collections.Generic;
using DataAccessInterfaces;
using Model;

namespace ProviderDao.Implementation
{
    public class ProviderTaskDao : ITaskDao
    {
        private List<Task> tasks;
        private static ITaskDao instance;

        private ProviderTaskDao() { }

        internal static ITaskDao Instance => instance ?? (instance = new ProviderTaskDao());
        
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
