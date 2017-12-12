using System.Collections.Generic;
using DataAccessInterfaces;
using Model;

namespace Oracle.Dao
{
    public class OracleTaskDao : ITaskDao
    {
        public IReadOnlyList<Task> AllTasks
        {
            get
            {
                return null;
                // TODO: Finish.
            }
        }
    }
}
