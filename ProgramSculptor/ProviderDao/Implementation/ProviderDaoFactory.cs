using DataAccessInterfaces;

namespace ProviderDao.Implementation
{
    public class ProviderDaoFactory : IDaoFactory
    {
        public ITaskDao TaskDao => ProviderTaskDao.Instance;
        public IClassFileDao ClassFileDao => ProviderClassFileDao.Instance;
        public ISolutionDao SolutionDao => ProviderSolutionDao.Instance;
        public IUserDao UserDao => ProviderUsersDao.Instance;
    }
}