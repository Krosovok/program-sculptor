using System;

namespace FileManaging
{
    public class FileSystemException : Exception
    {
        public FileSystemException(string message, Exception e) : base(message, e) { }
    }
}