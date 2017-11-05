using System.Windows;

namespace UI.Controls.Events
{
    public class TaskEventArgs : RoutedEventArgs
    {
        // TODO: Add data about selected task.

        public TaskEventArgs(RoutedEvent routedEvent, object source) 
            : base(routedEvent, source)
        {

        }
    }
}
