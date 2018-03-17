using System;
using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class TaskFileReader
    {
        private readonly Task task;

        public TaskFileReader(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            
            this.task = task;
        }

        public string GetTestSource(string fileName)
        {
            return ReadAllText(TestsFolder, fileName);
        }

        public string GetGivenTypeSource(string fileName)
        {
            return ReadAllText(GivenTypesFolder, fileName);
        }

        public string GetSolutionFileSource(string solutionName, string fileName)
        {
            string path = BuildFilePath(TasksFolder, task.TaskName, Solutions, solutionName, fileName);
            return ReadFile(path);
        }

        private string ReadAllText(string folder, string fileName)
        {
            string path = BuildFilePath(TasksFolder, task.TaskName, folder, fileName);
            return ReadFile(path);
        }

        private static string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (SystemException e)
            {
                throw new FileSystemException("File can't be read.", e);
            }
        }
    }
}
