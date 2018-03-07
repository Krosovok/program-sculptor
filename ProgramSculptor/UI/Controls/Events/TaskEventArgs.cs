using System.Windows;
using Model;
using ViewModel.Core;

namespace UI.Controls.Events
{
    public class TaskEventArgs : RoutedEventArgs
    {
        public TaskEventArgs(RoutedEvent routedEvent, object source, TaskViewModel selected) 
            : base(routedEvent, source)
        {
            Selected = selected;
        }
        
        public TaskViewModel Selected { get; }

    }
}
