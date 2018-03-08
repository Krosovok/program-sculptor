namespace SourceShowerViewModel
{
    public class SourceViewModel
    {
        public SourceViewModel(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
        
        public string FileName { get; }
        public string Content { get; }
    }
}
