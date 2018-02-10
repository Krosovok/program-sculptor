using System;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderDao;
using ProviderDao.Implementation;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderUserDaoTest
    {
        [TestMethod]
        public void TestUserFinding()
        {
            IUserDao usersDao = Dao.Factory.UserDao;

            bool success = usersDao.Login("Alter", "Alter");
            
            Assert.IsTrue(success);
        }
    }
}
