using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using static Test.DB.ProviderDaoTest.ProviderSolutionDaoIntegrationTest;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderClassFileDaoIntegrationTest
    {
        private readonly Solution solution = Dao.Factory.SolutionDao.GetUserTaskSolutions(
                Dao.Factory.TaskDao.AllTasks.First(),
                UserNameForAddDelete)
            .First(solution => solution.Name == TestSolutionName);

        private readonly ClassFile classFile = new ClassFile("/Test.cs");
        private readonly IClassFileDao classFileDao = Dao.Factory.ClassFileDao;

        private bool FileExistsInSolution
        {
            get
            {
                IEnumerable<ClassFile> solutionFiles = Dao.Factory.ClassFileDao.SolutionFiles(solution);
                return solutionFiles
                    .Any(file => 
                        file.FileName == classFile.FileName.TrimStart('/'));
            }
        }

        [Ignore]
        [TestMethod]
        public void TestAddFileToSolution()
        {
            classFileDao.AddFileToSolution(solution, classFile);

            Assert.IsTrue(FileExistsInSolution);
        }

        [Ignore]
        [TestMethod]
        public void TestDeleteFileFromSolution()
        {
            if (!FileExistsInSolution)
            {
                Assert.Fail();
            }

            Dao.Factory.ClassFileDao.DeleteFileFromSolution(solution, classFile);

            Assert.IsTrue(!FileExistsInSolution);
        }
    }
}