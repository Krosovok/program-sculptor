using System.Windows;
using System.IO;
using System.Text;
using System.Windows.Documents;
using UI.Content;

namespace UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для SourcePresenter.xaml
    /// </summary>
    public partial class SourcePresenter : Window
    {
        private const string Default = "file name";
        private const string TextContent = "text";

        public SourcePresenter() : this(
            Default, new MemoryStream(
                Encoding.Default.GetBytes(TextContent)))
        {

        }

        private SourcePresenter(string fileName, Stream sourceText)
        {
            InitializeComponent();

            FileName.Text = fileName;
            DocumentBuilder builder = new DocumentBuilder()
                .AddContent(sourceText);
            Source.Document = builder.Document;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
