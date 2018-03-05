using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class AllTasks : INotifyPropertyChanged
    {
        public AllTasks()
        {
            IReadOnlyList<Task> tasks = Dao.Factory.TaskDao.AllTasks;
            Tasks = tasks.Select(task => new TaskSolutions(task)).ToList();
            SelectTaskCommand = new RelayCommand<TaskSolutions>(SelectTask);
        }
        
        public IEnumerable<TaskSolutions> Tasks { get; }
        public object SelectedTaskViewModel { get; private set; } = new TaskSolutions();
        public ICommand SelectTaskCommand { get; }

        private void SelectTask(TaskSolutions selected)
        {
            SelectTaskToShow(selected);
            
            foreach (TaskSolutions task in Tasks)
            {
                bool isSelectedTask = ReferenceEquals(task, selected);
                task.IsSelected = isSelectedTask;
            }
        }

        private void SelectTaskToShow(TaskSolutions selected)
        {
            if (!selected.IsSandbox)
            {
                SelectedTaskViewModel = new TaskViewModel(selected.Task);
            }
            else
            {
                SelectedTaskViewModel = new TaskSolutions();
            }
            OnPropertyChanged(nameof(SelectedTaskViewModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
