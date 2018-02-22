using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class FileReader
    {
        private readonly Task task;

        public FileReader(Task task)
        {
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
            return File.ReadAllText(path);
        }

        private string ReadAllText(string folder, string fileName)
        {
            string path = BuildFilePath(TasksFolder, task.TaskName, folder, fileName);
            return File.ReadAllText(path);
        }
    }
}
