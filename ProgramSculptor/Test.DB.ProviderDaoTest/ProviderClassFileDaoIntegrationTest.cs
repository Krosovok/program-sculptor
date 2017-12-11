using System;
using System.Linq;
using DataAccessInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Test.DB.ProviderDaoTest
{
    [TestClass]
    public class ProviderClassFileDaoIntegrationTest
    {
        private readonly Solution solution =
            ProviderSolutionDaoIntegrationTest.TestSolution;

        private readonly ClassFile classFile = new ClassFile("Test.cs");
        private readonly IClassFileDao classFileDao = Dao.Factory.ClassFileDao;

        private bool FileExistsInSolution =>
            Dao.Factory.ClassFileDao.SolutionFiles(solution)
                .Any(file => file.FileName == classFile.FileName);

        [TestMethod]
        public void TestAddFileToSolution()
        {
            classFileDao.AddFileToSolution(solution, classFile);

            Assert.IsTrue(FileExistsInSolution);
        }

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