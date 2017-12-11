using System.Collections.Generic;
using System.IO;
using static FileManaging.Folding;

namespace FileManaging
{
    public static class FolderContents
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
            return SolutionContents(taskName, solutionName);
        }

        public static IEnumerable<FileInfo> OthersSolutionFiles(int solutionId)
        {
            string fileName = string.Format(OthersSolutionFormat, solutionId);
            return FilesInFolder(OthersSolutions, fileName);
        }

        public static DirectoryInfo SolutionFolder(string taskName, string solutionName)
        {
            return GetDirectoryInfo(TasksFolder, taskName, Solutions, solutionName);
        }

        
        private static IEnumerable<FileInfo> TaskContents(string taskName, string lastFolder)
        {
            return FilesInFolder(TasksFolder, taskName, lastFolder);
        }

        private static IEnumerable<FileInfo> SolutionContents(string taskName, string solutionName)
        {
            return FilesInFolder(TasksFolder, taskName, Solutions, solutionName);
        }
        
        private static IEnumerable<FileInfo> FilesInFolder(params string[] pathParts)
        {
            DirectoryInfo directory = GetDirectoryInfo(pathParts);
            return directory.EnumerateFiles();
        }

        private static DirectoryInfo GetDirectoryInfo(params string[] pathParts)
        {
            string path = BuildPath(pathParts);
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory;
        }
    }
}
