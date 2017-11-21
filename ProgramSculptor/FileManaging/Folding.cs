using System.IO;
using System.Text;

namespace FileManaging
{
    internal static class Folding
    {
        public const string TasksFolder = "Tasks";
        public const string GivenTypesFolder = "GivenTypes";
        public const string TestsFolder = "Tests";

        //public Folding()
        //{
        //    if (!Directory.Exists(TasksFolder))
        //    {
        //        Directory.CreateDirectory(TasksFolder);
        //    }
        //}

        public static string BuildPath(params string[] a)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string pathPart in a)
            {
                builder.Append(pathPart).Append(Path.PathSeparator);
            }
            return builder.ToString();
        }
    }
}
