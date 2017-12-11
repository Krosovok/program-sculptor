using System;
using System.Collections.Generic;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderTaskDaoIntegrationTest
    {
        [TestMethod]
        public void TestReadAllTasks()
        {
            IReadOnlyList<Task> allTasks = Dao.Factory.TaskDao.AllTasks;
            
            foreach (Task task in allTasks)
            {
                Console.WriteLine(task.TaskName);
            }
            
            Assert.IsTrue(allTasks.Count != 0);
        }
    }
}
