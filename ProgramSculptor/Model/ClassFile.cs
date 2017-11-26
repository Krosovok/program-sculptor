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

        internal ClassFile(string fileName, string description)
        {
            FileName = fileName;
            Description = description;
        }

        public string FileName { get; }
        public string TypeName => GetTypeName();
        public string Description { get; }

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
            IEnumerable<FileInfo> codeFiles = allFiles.Where(file => !file.Name.EndsWith(DescriptionExtension));
            IEnumerable<FileInfo> descriptions = allFiles.Where(file => file.Name.EndsWith(DescriptionExtension));

            return codeFiles.Select(codeFile => NewClassFile(codeFile, descriptions));
        }

        private static ClassFile NewClassFile(FileSystemInfo codeFile, IEnumerable<FileInfo> descriptions)
        {
            string name = codeFile.Name;
            string description = GetDescription(descriptions, name);

            ClassFile newItem = new ClassFile(name, description);
            return newItem;
        }

        private static string GetDescription(IEnumerable<FileInfo> descriptions, string name)
        {
            FileInfo descFile = descriptions.FirstOrDefault(file => file.Name.StartsWith(name));
            return descFile != null ? 
                File.ReadAllText(descFile.FullName) :
                null;
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
