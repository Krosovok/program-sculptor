using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using DataAccessInterfaces;
using Services;
using ViewModel.Command;

namespace DialogViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private const string SuccessMessage = "Logged in successfully.";
        private const string NotFoundMessage = "Username not found.";
        
        public LoginViewModel()
        {
            LoginCheckCommand = new RelayCommand<object>(ExecuteLoginCheck);
        }
        
        public IMessageService MessageService { get; set; }
        public string Username { get; set; } = string.Empty;
        public SecureString Password { private get; set; } = new SecureString();
        public ICommand LoginCheckCommand { get; }
        public bool? Result { get; set; }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void ExecuteLoginCheck(object notNeeded)
        {
            IUserDao dao = Dao.Factory.UserDao;

            string message = GetMessage(dao);
            
            MessageService.Show(message);
        }

        private string GetMessage(IUserDao dao)
        {
            try
            {
                return TryGetMessage(dao);
            }
            catch (UnauthorizedAccessException e)
            {
                return e.Message;
            }
        }

        private string TryGetMessage(IUserDao dao)
        {
            bool success = dao.Login(Username,
                ConvertToUnsecureString(Password));
            if (success)
            {
                Result = true;
                OnPropertyChanged(nameof(Result));
            }
            return success ? SuccessMessage : NotFoundMessage;
        }

        private static string ConvertToUnsecureString(SecureString password)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(password);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
