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
        public TaskList()
        {
            InitializeComponent();
        }
        
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
