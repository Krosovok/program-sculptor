using System.Windows;
using System.Windows.Controls;
using UI.Controls;
using UI.Controls.Events;

namespace UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ShowSandbox();
        }

        private void ShowSandbox(object sender, RoutedEventArgs e)
        {
            ShowSandbox();
        }

        private void ShowSandbox()
        {
            ShownPanel = new SandboxPanel();
        }

        private void TaskSelected(object sender, TaskEventArgs args)
        {
            /*Task selected = args....();*/

            ShownPanel = new TaskPanel(/*selected*/);
        }

        private Control ShownPanel
        {
            set { SelectedInfo.Navigate(value); }
        }

    }
}
