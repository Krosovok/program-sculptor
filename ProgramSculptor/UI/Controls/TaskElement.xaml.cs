using System.Windows;
using System.Windows.Controls;
using Model;
using ViewModel.Core;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskElement.xaml
    /// </summary>
    public partial class TaskElement 
    {
        public TaskElement()
        {
            InitializeComponent();
        }
        
        private void SolutionSelected(object sender, RoutedEventArgs e)
        {
            ListBox listView = (ListBox)sender;
            Solution selected = (Solution)listView.SelectedItem;
            
            TaskSolutions taskSolutions = DataContext as TaskSolutions;
            if (selected != null)
            {
                taskSolutions?.SelectSolutionCommand.Execute(selected);
            }
        }
    }
}
