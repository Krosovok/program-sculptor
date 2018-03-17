using System;
using System.Collections.Generic;
using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class TaskFolderContents : FolderContents
    {
        private readonly Task task;

        public TaskFolderContents(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
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
    }
}
