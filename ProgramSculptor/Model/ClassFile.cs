using System;

namespace Model
{
    public class ClassFile
    {
        private const string FileExtensionSeparator = ".";

        public ClassFile(string fileName, string description)
        {
            FileName = fileName;
            Description = description;
        }

        public string FileName { get; }
        public string TypeName => GetTypeName();
        public string Description { get; }

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
