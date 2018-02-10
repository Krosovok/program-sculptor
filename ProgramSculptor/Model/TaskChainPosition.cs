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

        public IEnumerable<Task> AllBefore => NullIfEmpty(tasksLink.Take(PreviousTaskIndex));
        public Task Previous => taskIndex == 0 ? null : tasksLink[PreviousTaskIndex];
        public Task Current => tasksLink[taskIndex];
        public Task Next => taskIndex == tasksLink.Length ? null : tasksLink[NextTaskIndex];
        public IEnumerable<Task> AllAfter => NullIfEmpty(tasksLink.Skip(NextTaskIndex));

        private int PreviousTaskIndex => taskIndex - 1;
        private int NextTaskIndex => taskIndex + 1;

        private static IEnumerable<T> NullIfEmpty<T>(IEnumerable<T> enumerable)
        {
            return !enumerable.Any() ? null : enumerable;
        }
    }
}