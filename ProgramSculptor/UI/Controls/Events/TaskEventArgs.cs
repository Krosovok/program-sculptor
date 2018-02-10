using System.Windows;
using Model;

namespace UI.Controls.Events
{
    public class TaskEventArgs : RoutedEventArgs
    {
        public TaskEventArgs(RoutedEvent routedEvent, object source, Task selected) 
            : base(routedEvent, source)
        {
            Selected = selected;
        }
        
        public Task Selected { get; }

    }
}
