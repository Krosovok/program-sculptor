using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UI.Controls.Events;
using ViewModel.Core;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList
    {
        //public static readonly RoutedEvent SelectedEvent;
        //private TaskElement selected;

        //static TaskList()
        //{
        //    SelectedEvent = EventManager.RegisterRoutedEvent("SelectedTask",
        //        RoutingStrategy.Bubble, typeof(TaskEventHandler), typeof(TaskList));
        //}

        public TaskList()
        {
            InitializeComponent();
        }
        
        //private void TaskSelected(object sender, TaskEventArgs e)
        //{
        //    selected = (TaskElement) sender;
        //    UnselectOthers(selected);
        //    RaiseEvent(new TaskEventArgs(SelectedEvent, this, e.Selected));
        //}

        //private void UnselectOthers(object sender)
        //{
        //    foreach (TaskElement child in FindVisualChildren(Tasks))
        //    {
        //        CollapseOtherOne(sender, child);
        //    }
        //}

        //private IEnumerable<TaskElement> FindVisualChildren(DependencyObject control)
        //{
        //    List<TaskElement> res = new List<TaskElement>();

        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
        //    {
        //        DependencyObject child = VisualTreeHelper.GetChild(control, i);
        //        TaskElement taskElement = child as TaskElement;
        //        if (taskElement != null)
        //        {
        //            res.Add(taskElement);
        //        }
        //        else
        //        {
        //            res.AddRange(FindVisualChildren(child));
        //        }
        //    }

        //    return res;
        //}
        
        //private void CollapseOtherOne(object sender, TaskElement taskControl)
        //{
        //    if (taskControl != null && taskControl != sender)
        //    {
        //        taskControl.Collapse();
        //    }
        //}

        //public event TaskEventHandler Selected
        //{
        //    add
        //    {
        //        AddHandler(SelectedEvent, value);
        //    }
        //    remove
        //    {
        //        RemoveHandler(SelectedEvent, value);
        //    }
        //}
        private void OnTaskSelected(object sender, RoutedEventArgs e)
        {
            AllTasks allTasks = DataContext as AllTasks;
            ListBox listBox = sender as ListBox;
            if (allTasks != null && listBox != null)
            {
                TaskSolutions selected = listBox.SelectedItem as TaskSolutions;
                ICommand command = allTasks.SelectTaskCommand;
                if (command.CanExecute(selected))
                {
                    command.Execute(selected);
                }
            }
        }
    }
}
