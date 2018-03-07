using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using UI.Controls.Events;
using UI.Windows;
using ViewModel.Core;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskElement.xaml
    /// </summary>
    public partial class TaskElement 
    {
        // TODO: Make it call task selection command. 

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
