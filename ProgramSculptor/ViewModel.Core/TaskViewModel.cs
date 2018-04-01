using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;
using ViewModel.Types;

namespace ViewModel.Core
{
    public class TaskViewModel : ITaskDetailsViewModel, ITypesContainer, IWorkflowStep
    {
        public TaskViewModel(Task task)
        {
            this.Task = task;
            InChain = new TaskChainPosition(task);
            TrySetConnectedFilesLists();
            
            StartNewSolutionCommand = new RelayCommand<object>(
                o => OnStartNewSolution());
            ShowSourcesCommand = new RelayCommand<GivenTypeFile>(
                file => SourceShower.ShowSource(file.FileName, file.Content));
        }

        public Task Task { get; }
        public string TaskName => Task.TaskName;
        public string Description => Task.Description;
        public IEnumerable<ClassFileViewModel> Types { get; private set; }
        public IEnumerable<ClassFileViewModel> Tests { get; private set; }
        public TaskChainPosition InChain { get; }
        public ICommand StartNewSolutionCommand { get; }
        public ICommand ShowSourcesCommand { get; }
        public ISourceShowerService SourceShower { get; set; }
        public IMessageService MessageService { get; set; }

        protected virtual void OnStartNewSolution()
        {
            StartNewSolution?.Invoke(Task);
        }
        
        private void TrySetConnectedFilesLists()
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            try
            {
                SetConnectedFilesLists(dao);
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
            }
        }

        private void SetConnectedFilesLists(IClassFileDao dao)
        {
            Types = dao.GetGivenTypes(Task)
                .Select(classFile => new GivenTypeFile(Task, classFile, MessageService))
                .ToList();
            Tests = dao.GetTests(Task)
                .Select(classFile => new TestFile(Task, classFile, MessageService))
                .ToList();
        }

        public event Action<Task> StartNewSolution;
        public event Action<Task> GoToTask
        {
            add { InChain.GoToTask += value; }
            remove { InChain.GoToTask -= value; }
        }

        public void Update(IWorkflowStep previousStepData)
        {
            // Nothing to update.
        }

        public void Clear()
        {
            // Nothing to clear.
        }
    }
}
