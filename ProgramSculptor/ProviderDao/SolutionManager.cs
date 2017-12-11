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

        public static void SaveFile(Solution parent, string fileName)
        {
            string filePath = FilePath(parent, fileName);
            File.Create(filePath);
        }

        private static string FilePath(Solution parent, string fileName)
        {
            DirectoryInfo solutionFolder =
                FolderContents.SolutionFolder(
                    parent.Task.TaskName, parent.Name);

            string filePath = Path.Combine(
                solutionFolder.FullName,
                fileName);
            return filePath;
        }
    }
}