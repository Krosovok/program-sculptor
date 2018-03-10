using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TaskChain
    {
        private const string Message = "Task from different chain.";
        private readonly Task[] tasks;

        internal TaskChain(Task task)
        {
            tasks = new[] {task};
        }

        private TaskChain(IEnumerable<Task> tasks)
        {
            this.tasks = tasks.ToArray();
        }

        public Task this[int position] => tasks[position];
        public IEnumerable<Task> AllTasks => tasks;
        public int Length => tasks.Length;

        public static void RegisterInTaskChain(IEnumerable<Task> tasks)
        {
            TaskChain newChain = new TaskChain(tasks);
            foreach (Task task in newChain.tasks)
            {
                task.Chain = newChain;
            }
        }

        public int PositionOf(Task task)
        {
            int taskIndex = Array.IndexOf(tasks, task);
            if (taskIndex == -1)
            {
                throw new ArgumentException(Message);
            }

            return taskIndex;
        }

        public Task PreviousOf(Task task)
        {
            int positionOfGiven = PositionOf(task);

            return positionOfGiven > 0 ? 
                tasks[positionOfGiven - 1] :
                null;
        }
    }
}