using System;
using System.IO;

namespace FileManaging
{
    public class LoadedFileReader
    {
        private readonly string fullFolderName;

        public LoadedFileReader(string fullFolderName)
        {
            this.fullFolderName = fullFolderName;
            //Directory.CreateDirectory(fullFolderName);
        }

        public string ReadAllText(string fileName)
        {
            string fullName = Path.Combine(fullFolderName, fileName);
            try
            {
                return File.ReadAllText(fullName);
            }
            catch (SystemException e)
            {
                throw new FileSystemException("Can't read other solution file.", e);
            }
        }
    }
}
