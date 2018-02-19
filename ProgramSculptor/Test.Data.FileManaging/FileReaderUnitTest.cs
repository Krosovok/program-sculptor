using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManaging;
using Model;

namespace Test.Data.FileManaging
{
    [TestClass]
    public class FileReaderUnitTest
    {
        private const string FileName = "TestFile.test";
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private const string Tasks = "Tasks";
        private const string TaskName = "testTask";
        private const string Tests = "Tests";
        private static readonly string TestsDirectoryPath = Path.Combine(CurrentDirectory,
            Tasks, TaskName, Tests);
        private static readonly string FullPath = 
            Path.Combine(TestsDirectoryPath,
                    FileName);
        private const string TestContent = "Test TEst TEST tyse uuuyuuuuuuuuuuuuu";

        [TestInitialize]
        public void CreateTestFile()
        {
            Directory.CreateDirectory(TestsDirectoryPath);
            FileStream fileStream = File.Create(FullPath);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(TestContent);
            }
        }

        [TestMethod]
        public void TestReadAllText()
        {
            string actualContent = new FileReader(new Task(0, TaskName, ""))
                .GetTestSource(FileName);
            
            Assert.AreEqual(TestContent, actualContent);
        }

        [TestCleanup]
        public void DeleteTestFile()
        {
            File.Delete(FullPath);
        }
    }
}