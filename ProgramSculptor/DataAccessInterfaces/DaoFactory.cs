namespace DataAccessInterfaces
{
    public interface IDaoFactory
    {
        ITaskDao TaskDao { get; }
        IClassFileDao ClassFileDao { get; }
        ISolutionDao SolutionDao { get; }
        IUserDao UserDao { get; }
    }
}