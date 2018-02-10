using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using UI.Controls.Events;

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

        private void LoadData()
        {
            solutions.ItemsSource = Dao.Factory.SolutionDao.GetMyTaskSolutions(Task, Dao.Factory.UserDao.CurrentUser);
        }

        public void Collapse()
        {
            solutions.Visibility = Visibility.Collapsed;
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

        public void UpdateSolutions()
        {
            LoadData();
        }
    }
}
