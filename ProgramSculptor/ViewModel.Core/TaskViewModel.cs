using System;
using System.Collections.Generic;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class TaskViewModel : ITaskDetailsViewModel
    {
        public TaskViewModel(Task task)
        {
            this.Task = task;
            InChain = new TaskChainPosition(task);
            GivenTypes = Dao.Factory.ClassFileDao.GetGivenTypes(Task);
            StartNewSolutionCommand = new RelayCommand<object>(o => OnStartNewSolution());
        }

        public Task Task { get; }
        public string TaskName => Task.TaskName;
        public string Description => Task.Description;
        public IEnumerable<ClassFile> GivenTypes { get; }
        public TaskChainPosition InChain { get; }
        public ICommand StartNewSolutionCommand { get; }

        public event Action<Task> StartNewSolution;

        protected virtual void OnStartNewSolution()
        {
            StartNewSolution?.Invoke(Task);
        }
    }
}
