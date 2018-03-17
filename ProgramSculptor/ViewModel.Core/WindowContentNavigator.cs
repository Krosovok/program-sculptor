using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class WindowContentNavigator : INotifyPropertyChanged
    {
        private const string SolutionNameMessage = "Enter your solution's name:";
        private const string NameSolutionTitle = "Name new solution";
        private object content;
        private IDialogFactory dialogFactory;
        private ISourceShowerService sourceShower;
        private IMessageService messageService;

        public WindowContentNavigator()
        {
            Main = new MainViewModel();
            Content = Main;
            HomeCommand = new RelayCommand<object>(o => Content = Main);
            Main.OpenSolution += OpenSolution;
            Main.StartNewSolution += StartNewSolution;
        }


        public IMessageService MessageService
        {
            get { return messageService; }
            set
            {
                messageService = value;
                Main.MessageService = value;
            }
        }

        public ISourceShowerService SourceShower
        {
            get { return sourceShower; }
            set
            {
                sourceShower = value;
                Main.SourceShower = value;
            }
        }

        public IDialogFactory DialogFactory
        {
            get { return dialogFactory; }
            set
            {
                dialogFactory = value;
                Main.DialogFactory = value;
            }
        }
        public MainViewModel Main { get; }
        public object Content
        {
            get { return content; }
            private set
            {
                content = value; 
                OnPropertyChanged(nameof(Content));
            }
        }
        public ICommand HomeCommand { get; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenSolution(Solution solution)
        {
            Content = new SolutionNavigation(solution, this);
        }

        private void StartNewSolution(Task task)
        {
            if (task.Chain.PositionOf(task) == 0)
            {
                StartWithoutBaseTask(task);
            }
            else
            {
                StartWithBaseTask(task);
            }
        }

        private void StartWithoutBaseTask(Task task)
        {
            IDialog dialog = SolutionNameDialog();
            string name = dialog.ShowNameDialog();

            if (name != null)
            {
                Solution newSolution = new Solution(name, Main.UserSession.Username, task);
                StartSolution(newSolution);
            }
        }

        private void StartWithBaseTask(Task task)
        {
            ISolutionStartDialog dialog = DialogFactory.NewSolutionStartDialog(task);
            Solution newSolution = dialog.ShowDialog();

            if (newSolution != null)
            {
                StartSolution(newSolution);
            }
        }

        private void StartSolution(Solution newSolution)
        {
            ISolutionDao dao = Dao.Factory.SolutionDao;

            try
            {
                dao.AddSolution(newSolution);
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
            }
            catch (ArgumentException e)
            {
                MessageService.Show(e.Message);
            }
              
            Content = new SolutionNavigation(newSolution, this);
        }

        private IDialog SolutionNameDialog()
        {
            IDialog dialog = DialogFactory.NewDialog();
            dialog.Message = SolutionNameMessage;
            dialog.Title = NameSolutionTitle;
            return dialog;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
