using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Data.FileManaging
{
    [TestClass]
    public class FolderContentsUnitTest
    {
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
            FolderContents.SolutionFolder(TaskName, SolutionName);

            string currentDirectory = Directory.GetCurrentDirectory();
            string fullSolutionPath = Path.Combine(currentDirectory, Tasks, TaskName, Solutions, SolutionName);

            Assert.IsTrue(Directory.Exists(fullSolutionPath));
        }

        [TestMethod]
        public void TestGetAllSolutionFiles()
        {
            DirectoryInfo directoryInfo = SolutionFolder();
            CreateFileIn(directoryInfo.FullName);

            IEnumerable<FileInfo> allSolutionFiles = FolderContents.GetAllSolutionFiles(TaskName, SolutionName);

            Assert.IsTrue(allSolutionFiles.Any(file => file.Name.Equals(TestFile)));
        }

        [TestMethod]
        public void TestGetAllGivenTypes()
        {
            FolderContents.GetAllGivenTypes(TaskName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), Tasks, TaskName, GivenTypes);
            CreateFileIn(path);

            IEnumerable<FileInfo> allGivenTypes = FolderContents.GetAllGivenTypes(TaskName);

            Assert.IsTrue(allGivenTypes.Any(file => file.Name.Equals(TestFile)));
        }

        [TestMethod]
        public void TestGetAllTests()
        {
            FolderContents.GetAllTests(TaskName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), Tasks, TaskName, Tests);
            CreateFileIn(path);

            IEnumerable<FileInfo> allTests = FolderContents.GetAllTests(TaskName);

            Assert.IsTrue(allTests.Any(file => file.Name.Equals(TestFile)));
        }


        private static void CreateFileIn(string directory)
        {
            string filePath = Path.Combine(directory, TestFile);
            File.Create(filePath).Close();
        }

        private static DirectoryInfo SolutionFolder()
        {
            return FolderContents.SolutionFolder(TaskName, SolutionName);
        }
        
    }
}