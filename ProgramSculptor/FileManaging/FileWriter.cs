using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class FileWriter
    {
        private readonly Solution solution;

        public FileWriter(Solution solution)
        {
            this.solution = solution;
        }

        public void WriteToFile(ClassFile classFile, string content)
        {
            string path = BuildFilePath(
                TasksFolder,
                solution.Task.TaskName, 
                Solutions,
                solution.Name,
                classFile.FileName);
            File.WriteAllText(path, content);
        }
    }
}
