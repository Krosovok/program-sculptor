using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using DataAccessInterfaces;
using Services;
using ViewModel.Command;

namespace DialogViewModel
{
    public class LoginViewModel
    {
        private const string ResultCaption = "Result";
        private const string SuccessMessage = "Logged in successfully.";
        private const string NotFoundMessage = "Username not found.";
        
        public LoginViewModel()
        {
            LoginCheckCommand = new RelayCommand<object>(ExecuteLoginCheck);
        }
        
        public IMessageService MessageService { get; set; } 
        public string Username { get; set; }
        public SecureString Password { private get; set; }
        public ICommand LoginCheckCommand { get; }
        public bool? Result { get; set; }
        
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
    }
}
