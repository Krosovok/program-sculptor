using System;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class DaoUnitTest
    {
        private const string DaoClass =
                "ProviderDao.Implementation.ProviderDaoFactory, ProviderDao, " +
                "Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        [TestMethod]
        public void CreateDaoFactoryTest()
        {
            Type expectedType = Type.GetType(DaoClass);
            IDaoFactory factory = Dao.Factory;

            Assert.IsInstanceOfType(factory, expectedType);
        }
    }
}