using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TaskChainPosition
    {
        private readonly Task[] tasksLink;
        private readonly int taskIndex;

        internal TaskChainPosition(Task[] tasks, int taskIndex)
        {
            tasksLink = tasks;
            this.taskIndex = taskIndex;
        }

        public IEnumerable<Task> AllBefore => tasksLink.Take(PreviousIndex);
        public Task Previous => taskIndex == 0 ? null : tasksLink[PreviousIndex];
        public Task Current => tasksLink[taskIndex];
        public Task Next => taskIndex == tasksLink.Length ? null : tasksLink[NextTask];
        public IEnumerable<Task> AllAfter => tasksLink.Skip(NextTask);

        private int PreviousIndex => taskIndex - 1;
        private int NextTask => taskIndex + 1;
    }
}