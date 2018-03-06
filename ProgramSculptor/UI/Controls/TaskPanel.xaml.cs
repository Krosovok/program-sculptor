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
        public TaskPanel()
        {
            InitializeComponent();
        }
    }
}