using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccessInterfaces;

namespace UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        private const string ResultCaption = "Result";
        private const string SuccessMessage = "Logged in successfully.";
        private const string NotFoundMessage = "Username not found.";
        private string resultMessage;

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            Login();

            MessageBox.Show(resultMessage, ResultCaption);
        }

        private void Login()
        {
            string username = Username.Text;
            string password = Password.Password;

            TryLogin(username, password);
        }

        private void TryLogin(string username, string password)
        {
            try
            {
                bool success = Dao.Factory.UserDao.Login(username, password);
                resultMessage = success ? SuccessMessage : NotFoundMessage;

                if (success)
                {
                    DialogResult = true;
                }
            }
            catch (UnauthorizedAccessException exception)
            {
                resultMessage = exception.Message;
            }
        }
    }
}