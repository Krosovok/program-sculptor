using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ViewModel.Core;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList
    {
        private ICollectionView tasks;

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

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            tasks = CollectionViewSource.GetDefaultView(Tasks.ItemsSource);
            tasks.Filter = Filter;
            tasks.Refresh();
        }
        
        private bool Filter(object o)
        {
            TaskSolutions item = o as TaskSolutions;

            return item != null &&
                   TaskNameContains(item);
        }

        private bool TaskNameContains(TaskSolutions item)
        {
            return item.Task.TaskName
                       .IndexOf(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
