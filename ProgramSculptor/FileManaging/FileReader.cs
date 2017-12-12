using System.IO;
using static FileManaging.Folding;

namespace FileManaging
{
    public static class FileReader
    {
        public static string GetTestSource(string taskName, string fileName)
        {
            return ReadAllText(taskName, TestsFolder, fileName);
        }

        public static string GetGivenTypeSource(string taskName, string fileName)
        {
            return ReadAllText(taskName, GivenTypesFolder, fileName);
        }

        private static string ReadAllText(string taskName, string folder, string fileName)
        {
            string path = BuildFilePath(TasksFolder, taskName, folder, fileName);
            return File.ReadAllText(path);
        }
    }
}
