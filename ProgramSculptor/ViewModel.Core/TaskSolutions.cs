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
    public class TaskSolutions : INotifyPropertyChanged
    {
        public TaskSolutions() : this(Task.Sandbox) { }

        public TaskSolutions(Task task)
        {
            Task = task;
            SelectSolution = new RelayCommand<Solution>(LaunchSolution,
                given => Solutinos.Contains(given));

            string currentUser = ConnectWithCurrentUser();
            SetUserSolutions(currentUser);
            SelectTaskCommand = new RelayCommand<object>(OnTaskSelected);
        }

        public Task Task { get; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public IEnumerable<Solution> Solutinos { get; private set; }
        public ICommand SelectTaskCommand;
        private bool isSelected;
        public bool IsSandbox => Task.Sandbox.Equals(Task);

        private void OnTaskSelected(object o)
        {
            TaskSelected?.Invoke(this);
        }

        public ICommand SelectSolution { get; }

        private string ConnectWithCurrentUser()
        {
            IUserDao userDao = Dao.Factory.UserDao;
            return userDao.CurrentUser;
        }

        // TODO: Know, that there is an error in logic - this is never updated when the user changes!!!
        private void SetUserSolutions(string currentUser)
        {
            Solutinos = Dao.Factory
                .SolutionDao.GetUserTaskSolutions(Task, currentUser);
        }

        private void LaunchSolution(Solution solution)
        {
            throw new NotImplementedException();
        }

        public event Action<TaskSolutions> TaskSelected;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
