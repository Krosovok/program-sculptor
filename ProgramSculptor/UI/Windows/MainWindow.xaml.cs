using System.Windows;
using System.Windows.Controls;
using DataAccessInterfaces;
using Model;
using UI.Controls;
using UI.Controls.Events;
using ViewModel.Core;

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
        }
        
        //
        private void Test(object sender, RoutedEventArgs e)
        {
            // TODO: REmove this!!!
            //
            Solution solution = Dao.Factory.SolutionDao.GetUserTaskSolutions(new Task(1, "Task1", ""), "Alter")[0];
            TaskWindow window = new TaskWindow(solution);
            window.Show();
            //
        }
        //
    }
}
