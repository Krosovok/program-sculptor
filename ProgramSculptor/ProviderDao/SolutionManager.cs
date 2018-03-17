using System.IO;
using FileManaging;
using Model;

namespace ProviderDao
{
    internal static class SolutionManager
    {
        public static void DeleteFile(Solution parent, string fileName)
        {
            string filePath = FilePath(parent, fileName);
            File.Delete(filePath);
        }

        public static void CreateFile(Solution parent, string fileName)
        {
            string filePath = FilePath(parent, fileName);
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        private static string FilePath(Solution solution, string fileName)
        {
            DirectoryInfo solutionFolder =
                new TaskFolderContents(solution.Task)
                    .SolutionFolder(solution);

            string filePath = Path.Combine(
                solutionFolder.FullName,
                fileName.TrimStart('/'));
            return filePath;
        }
    }
}