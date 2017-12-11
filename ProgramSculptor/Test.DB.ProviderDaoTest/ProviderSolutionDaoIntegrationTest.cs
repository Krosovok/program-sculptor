using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderSolutionDaoIntegrationTest
    {
        private const string UserName = "Alter";
        private const int SolutionCount = 2;

        private const string UserNameForAddDelete = "Reno99";
        private const string TestSolutionName = "Test solution";

        private static readonly Task First = Dao.Factory.TaskDao.AllTasks.First();
        private readonly ISolutionDao solutionDao = Dao.Factory.SolutionDao;

        public static Solution TestSolution => new Solution(TestSolutionName, UserNameForAddDelete, First);

        private bool SolutionExists
        {
            get { return UserSolutions.Any<Solution>(solution => solution.Name == TestSolutionName); }
        }

        private IEnumerable<Solution> UserSolutions => solutionDao.GetMyTaskSolutions(First, UserNameForAddDelete);

        [TestMethod]
        public void TestGetMyTaskSolutions()
        {
            IDaoFactory factory = Dao.Factory;
            ITaskDao taskDao = factory.TaskDao;

            Task firstTask = taskDao.AllTasks.First(task => task.Id == 1);
            IReadOnlyCollection<Solution> userSolutions = solutionDao.GetMyTaskSolutions(firstTask, UserName);

            Assert.IsTrue(userSolutions.Count == SolutionCount);
        }

        [TestMethod]
        public void TestAddSolution()
        {
            solutionDao.AddSolution(TestSolution);

            Assert.IsTrue(SolutionExists);
        }


        [TestMethod]
        public void TestDeleteSolution()
        {
            if (!SolutionExists)
            {
                Assert.Fail();
            }

            solutionDao.DeleteSolution(TestSolution);

            Assert.IsTrue(!SolutionExists);
        }
    }
}