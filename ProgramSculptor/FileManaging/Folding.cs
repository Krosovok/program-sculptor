using System;
using System.IO;
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
