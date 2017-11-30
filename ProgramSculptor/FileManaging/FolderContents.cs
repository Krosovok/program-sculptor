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
            return TaskContents(taskName, TestsFolder);
        }

        public static IEnumerable<FileInfo> GetAllGivenTypes(string taskName)
        {
            return TaskContents(taskName, GivenTypesFolder);
        }

        public static IEnumerable<FileInfo> GetAllSolutionFiles(string taskName, string solutionName)
        {
            return FilesInFolder(TasksFolder, taskName, Solutions, solutionName);
        }

        public static IEnumerable<FileInfo> OthersSolutionFiles(int solutionId)
        {
            string fileName = string.Format(OthersSolutionFormat, solutionId);
            return FilesInFolder(OthersSolutions, fileName);
        }

        private static IEnumerable<FileInfo> TaskContents(string taskName, string lastFolder)
        {
            return FilesInFolder(taskName, lastFolder);
        }

        private static IEnumerable<FileInfo> FilesInFolder(params string[] pathParts)
        {
            string path = BuildPath(pathParts);
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.EnumerateFiles();
        }
    }
}
