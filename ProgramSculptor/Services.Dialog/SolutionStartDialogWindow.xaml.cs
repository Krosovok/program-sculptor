using System.Windows;

namespace Services.Dialog
{
    /// <summary>
    /// Логика взаимодействия для SolutionStartDialogWindow.xaml
    /// </summary>
    public partial class SolutionStartDialogWindow : Window
    {
        public SolutionStartDialogWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
