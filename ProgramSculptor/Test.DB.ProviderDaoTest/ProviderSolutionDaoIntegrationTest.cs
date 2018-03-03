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
        public const string UserNameForAddDelete = "Reno99";
        public const string TestSolutionName = "Test solution";
        
        private const string UserName = "Alter";
        private const int SolutionCount = 3;
        
        private static readonly Task First = Dao.Factory.TaskDao.AllTasks.First();
        private readonly ISolutionDao solutionDao = Dao.Factory.SolutionDao;
        private static Solution testSolution;

        public static Solution TestSolution => testSolution ?? 
                                               (testSolution = new Solution(TestSolutionName, UserNameForAddDelete, First));

        private bool SolutionExists
        {
            get { return UserSolutions.Any<Solution>(solution => solution.Name == TestSolutionName); }
        }

        private IEnumerable<Solution> UserSolutions => solutionDao.GetUserTaskSolutions(First, UserNameForAddDelete);

        [TestMethod]
        public void TestGetMyTaskSolutions()
        {
            IDaoFactory factory = Dao.Factory;
            ITaskDao taskDao = factory.TaskDao;

            Task firstTask = taskDao.AllTasks.First(task => task.Id == 1);
            IReadOnlyCollection<Solution> userSolutions = solutionDao.GetUserTaskSolutions(firstTask, UserName);

            Assert.IsTrue(userSolutions.Count == SolutionCount);
        }

        [Ignore]
        [TestMethod]
        public void TestAddSolution()
        {
            solutionDao.AddSolution(TestSolution);

            Assert.IsTrue(SolutionExists);
            Assert.AreNotEqual(-1, TestSolution.Id);
        }

        // TODO: Fail????
        [Ignore]
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