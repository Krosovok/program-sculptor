using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Model;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class TaskChainPosition : INotifyPropertyChanged
    {
        private TaskChain chain;
        private int taskIndex;

        public TaskChainPosition(Task task)
        {
            this.Current = task;
            ChangeTaskCommand = new RelayCommand<Task>(
                other => Current = other,
                other => chain.AllTasks.Contains(other));
            GoToTaskCommand = new RelayCommand<object>(
                OnGoToTask);
        }

        private void OnGoToTask(object o)
        {
            GoToTask?.Invoke(Current);
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

        public IEnumerable<Task> AllBefore => NullIfEmpty(chain.AllTasks.Take(PreviousTaskIndex));
        public Task Previous => taskIndex == 0 ? null : chain[PreviousTaskIndex];
        public Task Current
        {
            get { return chain[taskIndex]; }
            private set
            {
                chain = value.Chain;
                taskIndex = chain.PositionOf(value);

                PropertiesChanged();
            }
        }
        public Task Next => taskIndex == chain.Length - 1 ? null : chain[NextTaskIndex];
        public IEnumerable<Task> AllAfter => NullIfEmpty(chain.AllTasks.Skip(NextTaskIndex + 1));
        public ICommand ChangeTaskCommand { get; }
        public ICommand GoToTaskCommand { get; }
        public bool HasChain => chain.Length > 1;

        private int PreviousTaskIndex => taskIndex - 1;
        private int NextTaskIndex => taskIndex + 1;

        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

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
        public event Action<Task> GoToTask;
    }
}