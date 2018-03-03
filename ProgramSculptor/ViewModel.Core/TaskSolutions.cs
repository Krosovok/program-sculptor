using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class TaskSolutions
    {
        public TaskSolutions() : this(Task.Sandbox) { }

        public TaskSolutions(Task task)
        {
            Task = task;
            SelectSolution = new RelayCommand<Solution>(LaunchSolution,
                given => Solutinos.Contains(given));

            string currentUser = ConnectWithCurrentUser();
            SetUserSolutions(currentUser);
        }

        public Task Task { get; }
        public IEnumerable<Solution> Solutinos { get; private set; }
        public ICommand SelectSolution { get; }

        private string ConnectWithCurrentUser()
        {
            IUserDao userDao = Dao.Factory.UserDao;
            return userDao.CurrentUser;
        }

        private void SetUserSolutions(string currentUser)
        {
            Solutinos = Dao.Factory
                .SolutionDao.GetUserTaskSolutions(Task, currentUser);
        }

        private void LaunchSolution(Solution solution)
        {
            throw new NotImplementedException();
        }
    }
}
