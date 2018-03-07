using System;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using Oracle.ManagedDataAccess.Client;

namespace ProviderDao.Implementation
{
    public class ProviderUsersDao : IUserDao
    {
        internal const string GuestUser = "Guest";
        private const string SqlKey = "ProviderUserDao.Login";
        private const string User = "user";
        private const string Pass = "passHash";
        private const string UserName = "user_name";

        private static IUserDao instance;


        private ProviderUsersDao()
        {
            CurrentUser = GuestUser;
        }

        internal static IUserDao Instance => instance ?? (instance = new ProviderUsersDao());

        public string CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            DbCommand loginFunction = BuildLoginFunction(username, password);

            ExecuteFunction(loginFunction);

            bool success = CheckSuccess(loginFunction);
            if (success)
            {
                CurrentUser = username;
            }
            return success;
        }
        
        private static void ExecuteFunction(DbCommand loginFunction)
        {
            try
            {
                loginFunction.ExecuteNonQuery();
            }
            catch (DbException e)
            {
                throw new UnauthorizedAccessException(e.Message, e);
            }
            finally
            {
                Db.Instance.CloseCommand(loginFunction);
            }
        }

        private static bool CheckSuccess(DbCommand loginFunction)
        {
            return loginFunction.Parameters[User].Value != DBNull.Value;
        }

        private static DbCommand BuildLoginFunction(string username, string password)
        {
            Db db = Db.Instance;

            DbCommand loginFunction = db.CreatePrecedureCommand(SqlKey);

            int passHash = password.GetHashCode();
            loginFunction.Parameters.AddRange(new DbParameter[]
            {
                db.CreateReturnParameter(DbType.String, User),
                db.CreateParameter(username, DbType.String, UserName),
                db.CreateParameter(passHash, DbType.Int32, Pass)
            });
            return loginFunction;
        }
    }
}