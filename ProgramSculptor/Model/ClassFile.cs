using System;

namespace Model
{
    public class ClassFile : IComparable<ClassFile>
    {
        private const string FileExtensionSeparator = ".";

        public ClassFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
        public string TypeName => GetTypeName();

        private string GetTypeName()
        {
            // TODO: Check it for logic! May be in reverce.
            return FileName.Remove(
                FileName.LastIndexOf(
                    FileExtensionSeparator,
                    StringComparison.Ordinal));
        }

        public int CompareTo(ClassFile other)
        {
            return string.Compare(
                this.FileName,
                other.FileName,
                StringComparison.Ordinal);
        }
    }
}