using System.Windows.Input;
using DataAccessInterfaces;

namespace ViewModel.Core
{
    public class UserSession
    {
        private readonly IUserDao userDao;
        
        public UserSession()
        {
            userDao = Dao.Factory.UserDao;
        }

        public string Username => userDao.CurrentUser;
        public ICommand LoginCommand { get; }
    }
}
