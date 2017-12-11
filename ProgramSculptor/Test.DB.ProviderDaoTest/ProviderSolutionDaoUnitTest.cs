using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderSolutionDaoUnitTest
    {
        private const string UserName = "Alter";
        private const int SolutionCount = 2;

        [TestMethod]
        public void TestGetMyTaskSolutions()
        {
            IDaoFactory factory = Dao.Factory;
            ITaskDao taskDao = factory.TaskDao;
            ISolutionDao solutionDao = factory.SolutionDao;

            Task first = taskDao.AllTasks.First(task => task.Id == 1);
            IReadOnlyList<Solution> userSolutions = solutionDao.GetMyTaskSolutions(first, UserName);

            Assert.IsTrue(userSolutions.Count == SolutionCount);
        }

        [TestMethod]
        public void TestAddSolution()
        {
            //Dao.Factory.SolutionDao.AddSolution();
            Assert.Fail();
        }

        [TestMethod]
        public void TestDeleteSolution()
        {
            //Dao.Factory.SolutionDao.DeleteSolution();
            Assert.Fail();
        }
    }
}