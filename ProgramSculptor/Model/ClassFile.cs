using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManaging;

namespace Model
{
    public class ClassFile
    {
        private const string FileExtensionSeparator = ".";
        private const string DescriptionExtension = FileExtensionSeparator + "desc";

        internal ClassFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
        public string TypeName => GetTypeName();

        public static IEnumerable<ClassFile> GetTests(string taskName)
        {
            IEnumerable<FileInfo> allFiles = FolderContents.GetAllTests(taskName);

            return ClassFiles(allFiles);
        }

        public static IEnumerable<ClassFile> GetGivenTypes(string taskName)
        {
            IEnumerable<FileInfo> allFiles = FolderContents.GetAllGivenTypes(taskName);

            return ClassFiles(allFiles);
        }

        private static IEnumerable<ClassFile> ClassFiles(IEnumerable<FileInfo> allFiles)
        {
            return allFiles.Select(NewClassFile);
        }

        private static ClassFile NewClassFile(FileSystemInfo codeFile)
        {
            string name = codeFile.Name;

            ClassFile newItem = new ClassFile(name);
            return newItem;
        }
        
        private string GetTypeName()
        {
            // TODO: Check it for logic.
            return FileName.Remove(
                FileName.LastIndexOf(
                    FileExtensionSeparator,
                    StringComparison.Ordinal));
        }
    }
}
