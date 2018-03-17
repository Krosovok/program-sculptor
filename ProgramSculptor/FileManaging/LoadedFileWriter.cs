using System;
using System.IO;

namespace FileManaging
{
    public class LoadedFileWriter
    {
        private readonly string fullFolderName;

        public LoadedFileWriter(string fullFolderName)
        {
            this.fullFolderName = fullFolderName;
            Directory.CreateDirectory(fullFolderName);
        }

        public void WriteToFile(string fileName, string fileContents)
        {
            string fullName = Path.Combine(fullFolderName, fileName);
            try
            {
                File.WriteAllText(fullName, fileContents);
            }
            catch (SystemException e)
            {
                throw new FileSystemException("Can't write to other solution file: " + fileName, e);
            }
        }
    }
}