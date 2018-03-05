using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using UI.Controls.Events;
using ViewModel;
using ViewModel.Core;
using Task = Model.Task;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskPanel.xaml
    /// </summary>
    public partial class TaskPanel 
    {
        private const string SolutionNameInputKey = "NewSolutionTextBox";

        public static readonly RoutedEvent NewSolutionEvent;

        static TaskPanel()
        {
            NewSolutionEvent = EventManager.RegisterRoutedEvent(
                nameof(NewSolutionEvent),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TaskPanel));
        }

        public TaskPanel()
        {
            InitializeComponent();
        }
        
        private void StartNewSolution(object sender, RoutedEventArgs e)
        {
            DockPanel content = (DockPanel) Resources[SolutionNameInputKey];
            NewSolutionActionFrame.Navigate(content);
        }
        
        // TODO: InCommand.
        public event RoutedEventHandler NewSolution
        {
            add { AddHandler(NewSolutionEvent, value); }
            remove { RemoveHandler(NewSolutionEvent, value); }
        }
    }
}