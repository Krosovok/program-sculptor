using System;
using System.IO;

namespace Model
{
    public class ClassFile : IComparable<ClassFile>
    {
        public ClassFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
        public string TypeName => GetTypeName();

        public int CompareTo(ClassFile other)
        {
            return string.Compare(
                this.FileName,
                other.FileName,
                StringComparison.Ordinal);
        }

        private string GetTypeName()
        {
            return Path.GetFileNameWithoutExtension(FileName);
        }
    }
}