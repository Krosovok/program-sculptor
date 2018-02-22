using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Model;

namespace ViewModel
{
    public class TaskChainPosition : INotifyPropertyChanged
    {
        private TaskChain chain;
        private int taskIndex;

        public TaskChainPosition(Task task)
        {
            this.Task = task;
        }

        public int TaskIndex
        {
            get { return taskIndex; }
            set
            {
                taskIndex = value; 
                PropertiesChanged();
            }
        }

        public Task Task
        {
            get { return chain[taskIndex]; }
            set
            {
                chain = value.Chain;
                taskIndex = chain.PositionOf(value);
                
                PropertiesChanged();
            }
        }

        public IEnumerable<Task> AllBefore => NullIfEmpty(chain.AllTasks.Take(PreviousTaskIndex));
        public Task Previous => taskIndex == 0 ? null : chain[PreviousTaskIndex];
        public Task Current => chain[taskIndex];
        public Task Next => taskIndex == chain.Length - 1 ? null : chain[NextTaskIndex];
        public IEnumerable<Task> AllAfter => NullIfEmpty(chain.AllTasks.Skip(NextTaskIndex + 1));

        private int PreviousTaskIndex => taskIndex - 1;
        private int NextTaskIndex => taskIndex + 1;

        private static IEnumerable<T> NullIfEmpty<T>(IEnumerable<T> enumerable)
        {
            return !enumerable.Any() ? null : enumerable;
        }

        private void PropertiesChanged()
        {
            OnPropertyChanged(
                nameof(AllBefore),
                nameof(Previous),
                nameof(Current),
                nameof(Next),
                nameof(AllAfter));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}