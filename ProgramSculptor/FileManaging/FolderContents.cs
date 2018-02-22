using System.Collections.Generic;
using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class FolderContents
    {
        private readonly Task task;

        public FolderContents(Task task)
        {
            this.task = task;
        }

        public IEnumerable<FileInfo> GetAllTests()
        {
            return TaskContents(TestsFolder);
        }

        public IEnumerable<FileInfo> GetAllGivenTypes()
        {
            return TaskContents(GivenTypesFolder);
        }

        public IEnumerable<FileInfo> GetAllSolutionFiles(Solution solution)
        {
            return SolutionContents(solution.Name);
        }

        // TODO: Chnage or remove. It does not belong to "task" in the same way...
        public static IEnumerable<FileInfo> OthersSolutionFiles(int solutionId)
        {
            string fileName = string.Format(OthersSolutionFormat, solutionId);
            return FilesInFolder(OthersSolutions, fileName);
        }

        public DirectoryInfo SolutionFolder(Solution solution)
        {
            return GetDirectoryInfo(TasksFolder, task.TaskName, Solutions, solution.Name);
        }
        
        private IEnumerable<FileInfo> TaskContents(string lastFolder)
        {
            return FilesInFolder(TasksFolder, task.TaskName, lastFolder);
        }

        private IEnumerable<FileInfo> SolutionContents(string solutionName)
        {
            return FilesInFolder(TasksFolder, task.TaskName, Solutions, solutionName);
        }
        
        private static IEnumerable<FileInfo> FilesInFolder(params string[] pathParts)
        {
            DirectoryInfo directory = GetDirectoryInfo(pathParts);
            return directory.EnumerateFiles();
        }

        private static DirectoryInfo GetDirectoryInfo(params string[] pathParts)
        {
            string path = BuildDirectoryPath(pathParts);
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory;
        }
    }
}
