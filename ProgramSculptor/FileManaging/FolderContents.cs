using System;
using System.Collections.Generic;
using System.IO;

namespace FileManaging
{
    public class FolderContents
    {
        public IEnumerable<FileInfo> FilesInFolder(params string[] pathParts)
        {
            try
            {
                DirectoryInfo directory = GetDirectoryInfo(pathParts);
                return directory.EnumerateFiles();
            }
            catch (SystemException e)
            {
                throw new FileSystemException("Failed to find files in folder.", e);
            }
        }

        public DirectoryInfo GetDirectoryInfo(params string[] pathParts)
        {
            string path = Folding.BuildDirectoryPath(pathParts);
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory;
        }
    }
}