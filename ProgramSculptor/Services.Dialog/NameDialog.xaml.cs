using System.Windows;

namespace Services.Dialog
{
    /// <summary>
    /// Логика взаимодействия для NameDialog.xaml
    /// </summary>
    public partial class NameDialog : Window
    {
        public NameDialog()
        {
            InitializeComponent();
        }

        public NameDialog(Dialog dialogData) : this()
        {
            DataContext = dialogData;
        }

        public string Message { get; set; } = "Enter your data.";

        private void OkClick(object sender, RoutedEventArgs e)
        {
            // TODO: It is possible that data will be lost. Check it.
            DialogResult = true;
        }
    }
}
