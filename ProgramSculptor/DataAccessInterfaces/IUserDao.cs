namespace DataAccessInterfaces
{
    public interface IUserDao
    {
        bool Login(string username, string password);

        string CurrentUser { get; }
    }
}
