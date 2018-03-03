using System.Windows;
using ViewModel.Core;
using ViewModel.Command;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для CodeArea.xaml
    /// </summary>
    public partial class CodeArea
    {
        public CodeArea()
        {
            InitializeComponent();
        }

        private void DropCommand(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);
            ((LoadedClasses)DataContext)?.AddFilesCommand.Execute(fileNames);
        }
    }
}
