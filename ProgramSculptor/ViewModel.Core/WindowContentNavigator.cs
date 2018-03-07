using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;
using Task = Model.Task;

namespace ViewModel.Core
{
    public class WindowContentNavigator : INotifyPropertyChanged
    {
        private const string SolutionNameMessage = "Enter your solution's name:";
        private const string NameSolutionTitle = "Name new solution";
        private object content;
        private IDialogFactory dialogFactory;
        private IMessageService messageService;

        public WindowContentNavigator()
        {
            Main = new MainViewModel();
            Content = Main;
            HomeCommand = new RelayCommand<object>(o => Content = Main);
            //StartSolutionCommand = new RelayCommand<Solution>(StartSolution);
            Main.OpenSolution += OpenSolution;
            Main.StartNewSolution += StartNewSolution;
        }


        public IMessageService MessageService
        {
            get { return messageService; }
            set { messageService = value; }
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
            Content = new SolutionNavigation(solution, MessageService, DialogFactory);
        }

        private void StartNewSolution(Task task)
        {
            IDialog dialog = SolutionNameDialog();
            string name = dialog.ShowNameDialog();

            if (name != null)
            {
                Solution newSolution = new Solution(name, Main.UserSession.Username, task);
                Dao.Factory.SolutionDao.AddSolution(newSolution);
                Content = new SolutionNavigation(newSolution, MessageService, DialogFactory);
            }
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
