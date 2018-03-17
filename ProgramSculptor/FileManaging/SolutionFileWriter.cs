using System;
using System.IO;
using Model;
using static FileManaging.Folding;

namespace FileManaging
{
    public class SolutionFileWriter
    {
        private readonly Solution solution;

        public SolutionFileWriter(Solution solution)
        {
            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }
            
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
            WriteAllText(content, path);
        }

        private static void WriteAllText(string content, string path)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (SystemException e)
            {
                throw new FileSystemException("Can't write to file.", e);
            }
        }
    }
}
