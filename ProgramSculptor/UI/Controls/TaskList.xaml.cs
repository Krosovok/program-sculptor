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
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        public static readonly RoutedEvent SelectedEvent;

        static TaskList()
        {
            SelectedEvent = EventManager.RegisterRoutedEvent("Selected",
                RoutingStrategy.Bubble, typeof(TaskEventHandler), typeof(TaskList));
        }

        public TaskList()
        {
            InitializeComponent();

            taskList.Items.Add("111");
            taskList.Items.Add("222");
            taskList.Items.Add("333");
            taskList.Items.Add("444");
        }

        private void TaskSelected(object sender, TaskEventArgs e)
        {
            UnselectOthers(sender);
            OnTaskSelected();
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

        private void OnTaskSelected()
        {
            TaskEventArgs args = new TaskEventArgs(SelectedEvent, this);
            RaiseEvent(args);
        }
        
        private void UnselectOthers(object sender)
        {
            foreach (TaskElement child in FindVisualChildren(taskList))
            {
                CollapseOtherOne(sender, child);
            }
        }

        private IEnumerable<TaskElement> FindVisualChildren(DependencyObject control)
        {
            List<TaskElement> res = new List<TaskElement>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                TaskElement taskElement = child as TaskElement;
                if (taskElement != null)
                {
                    res.Add(taskElement);
                }
                else
                {
                    res.AddRange(FindVisualChildren(child));
                }
            }

            return res;
        }

        private void CollapseOtherOne(object sender, TaskElement taskControl)
        {
            if (taskControl != null && taskControl != sender)
            {
                taskControl.Collapse();
            }
        }
    }
}
