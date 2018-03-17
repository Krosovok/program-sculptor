using System.Collections.Generic;
using System.IO;

namespace FileManaging
{
    public class OthersFolderContents : FolderContents
    {
        public OthersFolderContents(int solutionId)
        {
            SolutionId = solutionId;
            FolderName = string.Format(Folding.OthersSolutionFormat, SolutionId);
            FullFolderName = Folding.BuildDirectoryPath(
                Folding.OthersSolutions,
                FolderName);
        }

        public int SolutionId { get; }
        public bool IsLoaded => Directory.GetFiles(FullFolderName).Length != 0;
        private string FolderName { get; }
        private string FullFolderName { get; }

        public IEnumerable<FileInfo> OthersSolutionFiles()
        {
            return FilesInFolder(Folding.OthersSolutions, FolderName);
        }

        public void LoadFiles(Dictionary<string, string> fileNameToContents)
        {
            LoadedFileWriter fileWriter = new LoadedFileWriter(FullFolderName);
            
            foreach (KeyValuePair<string, string> pair in fileNameToContents)
            {
                string fileName = pair.Key;
                string fileContents = pair.Value;

                fileWriter.WriteToFile(fileName, fileContents);
            }
        }

        public string ReadFile(string fileName)
        {
            LoadedFileReader reader = new LoadedFileReader(FullFolderName);
            return reader.ReadAllText(fileName);
        }
    }
}