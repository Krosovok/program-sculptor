using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

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
