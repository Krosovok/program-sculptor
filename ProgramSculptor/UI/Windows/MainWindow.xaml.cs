using System.Windows;
using System.Windows.Controls;
using DataAccessInterfaces;
using Model;
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

            tasks.DataContext = Dao.Factory.TaskDao.AllTasks;
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
            Task selected = args.Selected;
            TaskPanel taskPanel = new TaskPanel(selected);
            taskPanel.NewSolution += UpdateSolutions;
            
            ShownPanel = taskPanel;
        }

        private void UpdateSolutions(object sender, RoutedEventArgs e)
        {
            tasks.UpdateSolutions();
        }

        private Control ShownPanel
        {
            set { SelectedInfo.Navigate(value); }
        }

    }
}
