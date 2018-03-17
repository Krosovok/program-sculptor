using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Test.Data.FileManaging
{
    [TestClass]
    public class TaskFolderContentsUnitTest
    {
        private static readonly Task Task = new Task(0, TaskName, "");
        private static readonly Solution Solution = new Solution(SolutionName, "", Task);
        private const string TaskName = "Sample Task";
        private const string SolutionName = "Sample Solution";

        private const string Tasks = "Tasks";
        private const string Solutions = "Solutions";
        private const string GivenTypes = "Given types";
        private const string Tests = "Tests";

        private const string TestFile = "Test.cs";

        [TestMethod]
        public void TestSolutionFolderCreating()
        {
            new TaskFolderContents(Task)
                .SolutionFolder(Solution);

            string currentDirectory = Directory.GetCurrentDirectory();
            string fullSolutionPath = Path.Combine(currentDirectory, Tasks, TaskName, Solutions, SolutionName);

            Assert.IsTrue(Directory.Exists(fullSolutionPath));
        }

        [TestMethod]
        public void TestGetAllSolutionFiles()
        {
            DirectoryInfo directoryInfo = SolutionFolder();
            CreateFileIn(directoryInfo.FullName);

            IEnumerable<FileInfo> allSolutionFiles = new TaskFolderContents(Task)
                .GetAllSolutionFiles(Solution);

            Assert.IsTrue(allSolutionFiles.Any(file => file.Name.Equals(TestFile)));
        }

        [TestMethod]
        public void TestGetAllGivenTypes()
        {
            new TaskFolderContents(Task).GetAllGivenTypes();
            string path = Path.Combine(Directory.GetCurrentDirectory(), Tasks, TaskName, GivenTypes);
            CreateFileIn(path);

            IEnumerable<FileInfo> allGivenTypes = new TaskFolderContents(Task).GetAllGivenTypes();

            Assert.IsTrue(allGivenTypes.Any(file => file.Name.Equals(TestFile)));
        }

        [TestMethod]
        public void TestGetAllTests()
        {
            new TaskFolderContents(Task).GetAllTests();
            string path = Path.Combine(Directory.GetCurrentDirectory(), Tasks, TaskName, Tests);
            CreateFileIn(path);

            IEnumerable<FileInfo> allTests = new TaskFolderContents(Task).GetAllTests();

            Assert.IsTrue(allTests.Any(file => file.Name.Equals(TestFile)));
        }


        private static void CreateFileIn(string directory)
        {
            string filePath = Path.Combine(directory, TestFile);
            File.Create(filePath).Close();
        }

        private static DirectoryInfo SolutionFolder()
        {
            return new TaskFolderContents(Task).SolutionFolder(Solution);
        }
        
    }
}