using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using DB.SqlFactory;
using Model;

namespace ProviderDao
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
                    new TaskReader().GetList();
                }

                return tasks.AsReadOnly();
            }
        }
    }
}
