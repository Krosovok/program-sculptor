using System;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderDaoFactoryUnitTest
    {
        private const string DaoClass = "ProviderDao.Implementation.ProviderSolutionDao, ProviderDao, " +
                                        "Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        [TestMethod]
        public void TestFacturingDao()
        {
            Type daoType = Type.GetType(
                DaoClass);
            ISolutionDao dao = Dao.Factory.SolutionDao;

            Assert.IsInstanceOfType(dao, daoType);
        }
    }
}