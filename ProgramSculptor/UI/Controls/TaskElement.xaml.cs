using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using UI.Controls.Events;
using UI.Windows;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskElement.xaml
    /// </summary>
    public partial class TaskElement : UserControl
    {
        public static readonly RoutedEvent SelectedEvent;

        static TaskElement()
        {
            SelectedEvent = EventManager.RegisterRoutedEvent("SelectedTask",
                RoutingStrategy.Bubble, typeof(TaskEventHandler), typeof(TaskElement));
        }

        public TaskElement()
        {
            InitializeComponent();

            Selected += TaskSelected;
        }

        public bool IsExpanded
        {
            get
            {
                return solutions.Visibility != Visibility.Collapsed;
            }
        }

        public event TaskEventHandler Selected
        {
            add
            {
                AddHandler(SelectedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedEvent, value);
            }
        }

        private Task Task => DataContext as Task;
        
        public void Expand()
        {
            solutions.Visibility = Visibility.Visible;
            LoadData();
        }

        public void Collapse()
        {
            solutions.Visibility = Visibility.Collapsed;
        }

        public void UpdateSolutions()
        {
            LoadData();
        }

        private void LoadData()
        {
            solutions.ItemsSource = Dao.Factory.SolutionDao.GetUserTaskSolutions(Task, Dao.Factory.UserDao.CurrentUser);
        }

        private void TaskClick(object sender, MouseButtonEventArgs e)
        {
            OnTaskSelected();
        }

        private void OnTaskSelected()
        {
            TaskEventArgs args = new TaskEventArgs(SelectedEvent, this, Task);
            RaiseEvent(args);
        }

        private void TaskSelected(object sender, TaskEventArgs args)
        {
            if (!IsExpanded)
            {
                Expand();
            }
            else
            {
                Collapse();
            }
        }

        private void SolutionSelected(object sender, RoutedEventArgs e)
        {
            ListBox listView = (ListBox) sender;
            Solution selected = (Solution)listView.SelectedItem;
            
            Window solutionWindow = new TaskWindow(selected);
            solutionWindow.Show();
            
            // TODO: Do same in "Start new".
        }
    }
}
