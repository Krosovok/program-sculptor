using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataAccessInterfaces;
using Services;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class UserSession : INotifyPropertyChanged
    {
        private readonly IUserDao userDao;
        
        public UserSession()
        {
            userDao = Dao.Factory.UserDao;
            LoginCommand = new RelayCommand<object>(ExecuteLogin);
        }

        public string Username => userDao.CurrentUser;
        public ICommand LoginCommand { get; }
        public IDialogFactory DialogFactory { get; set; }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void ExecuteLogin(object o)
        {
            bool success = DialogFactory.NewDialog().ShowLoginDialog();
            if (success)
            {
                OnPropertyChanged(nameof(this.Username));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
