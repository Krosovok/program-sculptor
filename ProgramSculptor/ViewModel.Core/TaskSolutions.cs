using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class TaskSolutions : INotifyPropertyChanged, ITaskDetailsViewModel
    {
        private bool isSelected;
        private IEnumerable<Solution> solutinos;
        private IMessageService messageService;

        public TaskSolutions() : this(Task.Sandbox) { }

        public TaskSolutions(Task task)
        {
            Task = task;
            SelectSolutionCommand = new RelayCommand<Solution>(LaunchSolution,
                given => Solutinos.Contains(given));
            StartNewSolutionCommand = new RelayCommand<object>(NewSolution);

            //ChangeToCurrentUser();
        }

        public Task Task { get; }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                if (value)
                {
                    ChangeToCurrentUser();
                }
            }
        }

        public IEnumerable<Solution> Solutinos
        {
            get
            {
                if (solutinos == null && MessageService != null)
                {
                    ChangeToCurrentUser();
                }
                
                return solutinos;
            }
            private set { solutinos = value; }
        }

        public bool IsSandbox => Task.Sandbox.Equals(Task);
        public ICommand SelectSolutionCommand { get; }
        public ICommand StartNewSolutionCommand { get; }

        public IMessageService MessageService
        {
            get { return messageService; }
            set
            {
                messageService = value; 
                OnPropertyChanged(nameof(Solutinos));
            }
        }

        public void ChangeToCurrentUser()
        {
            string currentUser = ConnectWithCurrentUser();
            SetUserSolutions(currentUser);
        }

        private string ConnectWithCurrentUser()
        {
            IUserDao userDao = Dao.Factory.UserDao;
            return userDao.CurrentUser;
        }
        
        private void SetUserSolutions(string currentUser)
        {
            ISolutionDao dao = Dao.Factory.SolutionDao;
            Solutinos = TryGetUserTaskSolutions(currentUser, dao);
            OnPropertyChanged(nameof(Solutinos));
        }

        private IEnumerable<Solution> TryGetUserTaskSolutions(string currentUser, ISolutionDao dao)
        {
            try
            {
                return dao.GetUserTaskSolutions(Task, currentUser);
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
                return new Solution[0];
            }
        }

        private void LaunchSolution(Solution solution)
        {
            OpenSolution?.Invoke(solution);
        }

        private void NewSolution(object o)
        {
            StartNewSolution?.Invoke(Task);
        }

        public event Action<Solution> OpenSolution;
        public event Action<Task> StartNewSolution;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
