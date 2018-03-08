using System.Windows;

namespace Services.SourceShower
{
    /// <summary>
    /// Логика взаимодействия для SourcePresenter.xaml
    /// </summary>
    public partial class SourcePresenter : Window
    {
        public SourcePresenter()
        {
            InitializeComponent();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
