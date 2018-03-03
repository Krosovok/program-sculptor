using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ViewModel;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для SandboxPanel.xaml
    /// </summary>
    public partial class SandboxPanel : UserControl
    {
        public SandboxPanel()
        {
            InitializeComponent();

            //DataContext = new SandboxViewModel();

            //(Application.Current as App).LanguageChanged += ChangeLanguage;
            //// TODO: Add I18n support.
        }

        //private void ChangeLanguage(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void NewSandbox(object sender, RoutedEventArgs e)
        {
            // TODO: Start new sandbox here.
        }
    }
}
