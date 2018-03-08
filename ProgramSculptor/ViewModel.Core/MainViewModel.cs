using System;
using System.Windows.Input;
using Model;
using Services;

namespace ViewModel.Core
{
    public class MainViewModel
    {
        private IDialogFactory dialogFactory;
        private ISourceShowerService sourceShower;

        public MainViewModel()
        {
            Tasks.OpenSolution += OnOpenSolution;
            Tasks.StartNewSolution += OnStartNewSolution;
        }
        
        public AllTasks Tasks { get; } = new AllTasks();
        public UserSession UserSession { get; } = new UserSession();
        public IDialogFactory DialogFactory
        {
            get { return dialogFactory; }
            set
            {
                dialogFactory = value;
                UserSession.DialogFactory = value;
            }
        }

        public ISourceShowerService SourceShower
        {
            get { return sourceShower; }
            set
            {
                sourceShower = value;
                Tasks.SourceShower = value;
            }
        }


        protected virtual void OnOpenSolution(Solution solution)
        {
            OpenSolution?.Invoke(solution);
        }

        protected virtual void OnStartNewSolution(Task obj)
        {
            StartNewSolution?.Invoke(obj);
        }
        
        public event Action<Solution> OpenSolution;
        public event Action<Task> StartNewSolution;

    }
}
