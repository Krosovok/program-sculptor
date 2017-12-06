using DataAccessInterfaces;

namespace ProviderDao
{
    public class ProviderDaoFactory : IDaoFactory
    {
        public ITaskDao TaskDao => new ProviderTaskDao();
        public IClassFileDao ClassFileDao => new ProviderClassFileDao();
        public ISolutionDao SolutionDao => new ProviderSolutionDao();
    }
}