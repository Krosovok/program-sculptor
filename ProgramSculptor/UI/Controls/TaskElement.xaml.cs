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
            SelectedEvent = EventManager.RegisterRoutedEvent("Selected",
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

        public void Expand()
        {
            solutions.Visibility = Visibility.Visible;
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
            TaskEventArgs args = new TaskEventArgs(SelectedEvent, this);
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
        
    }
}
