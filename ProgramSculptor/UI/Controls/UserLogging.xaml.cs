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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessInterfaces;
using UI.Windows;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserLoggin.xaml
    /// </summary>
    public partial class UserLoggin : UserControl
    {
        private string username;

        public UserLoggin()
        {
            InitializeComponent();
            Username = Dao.Factory.UserDao.CurrentUser;
        }

        private string Username
        {
            get { return username; }
            set
            {
                username = value;
                Welcome.Text = $"You are logged as {value}.";
            }
        }

        //private void LoginClick(object sender, RoutedEventArgs e)
        //{
        //    LoginDialog dialog = new LoginDialog();
        //    bool? result = dialog.ShowDialog();
        //    if (result == true)
        //    {
        //        Username = Dao.Factory.UserDao.CurrentUser;
        //    }
        //}
    }
}
