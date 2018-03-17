using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Data.FileManaging
{
    [TestClass]
    public class OthersFolderContentsTest
    {
        private const string FileName = "file.cs";
        private const string Content = "content";
        private const string SolutionFolder = "Solution_132";

        private readonly string fullPath = Path.Combine(Directory.GetCurrentDirectory(),
            Folding.OthersSolutions,
            SolutionFolder,
            FileName);

        private readonly Dictionary<string, string> files = new Dictionary<string, string>();
        private readonly OthersFolderContents othersSolution = new OthersFolderContents(132);

        [TestInitialize]
        public void InitData()
        {
            files.Add(FileName, Content);

            string path = Path.Combine(Directory.GetCurrentDirectory(),
                Folding.OthersSolutions,
                SolutionFolder);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        [TestMethod]
        public void TestOthersSolutionFiles()
        {
            Load();

            IEnumerable<FileInfo> othersSolutionFiles = othersSolution.OthersSolutionFiles();

            Assert.IsTrue(othersSolutionFiles.Any(file => file.Name == FileName));
        }

        [TestMethod]
        public void TestLoadFiles()
        {
            Load();

            bool exists = File.Exists(fullPath);
            Assert.IsTrue(exists);
        }

        private void Load()
        {
            if (!othersSolution.IsLoaded)
            {
                othersSolution.LoadFiles(files);
            }
        }
    }
}