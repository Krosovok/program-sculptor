using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileManaging.Folding;

namespace FileManaging
{
    public class FolderContents
    {
        public static IEnumerable<FileInfo> GetAllTests(string taskName)
        {
            return AllFiles(taskName, TestsFolder);
        }

        public static IEnumerable<FileInfo> GetAllGivenTypes(string taskName)
        {
            return AllFiles(taskName, GivenTypesFolder);
        }

        private static IEnumerable<FileInfo> AllFiles(string taskName, string lastFolder)
        {
            string path = BuildPath(TasksFolder, taskName, lastFolder);
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.EnumerateFiles();
        }
    }
}
