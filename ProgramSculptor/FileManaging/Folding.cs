using System.IO;
using System.Linq;
using System.Text;

namespace FileManaging
{
    internal static class Folding
    {
        public const string TasksFolder = "Tasks";
        public const string GivenTypesFolder = "Given types";
        public const string TestsFolder = "Tests";
        public const string OthersSolutions = "Others solutions";
        public const string Solutions = "Solutions";
        public const string OthersSolutionFormat = "Solution_{0}";

        public static string BuildPath(params string[] parts)
        {
            string pathEnd = Path.Combine(parts);
            string baseDirectory = Directory.GetCurrentDirectory();

            string fullPath = Path.Combine(baseDirectory, pathEnd);

            CreateDirectoryIfNeeded(fullPath);
            
            return fullPath;
        }

        public static string BuildFilePath(params string[] pathParts)
        {
            string[] directoryParts = pathParts.Take(pathParts.Length - 1).ToArray();
            string directoryPath = BuildPath(directoryParts);

            string fullPath = Path.Combine(directoryPath, pathParts.Last().TrimStart('/'));

            CreateFileIfNeeded(fullPath);

            return fullPath;
        }

        private static void CreateDirectoryIfNeeded(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        private static void CreateFileIfNeeded(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
            }
        }
    }
}
