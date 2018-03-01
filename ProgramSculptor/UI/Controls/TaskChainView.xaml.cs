using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Model;
using UI.Content;
using ViewModel;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskChainView.xaml
    /// </summary>
    public partial class TaskChainView 
    {
        private const string NoChainText = "This task does not belong to any task chain.";

        private readonly FlowDocument noChainDisplay =
            new DocumentBuilder()
                .AddContent(NoChainText)
                .Document;

        private readonly ContextMenu otherTasksMenu;

        public TaskChainView()
        {
            InitializeComponent();
            // TODO: Do something to the Command...
            otherTasksMenu = (ContextMenu) Resources["OtherTaskMenu"];
        }
        
        private void ContextMenuOpenClick(object sender, RoutedEventArgs e)
        {
            otherTasksMenu.Visibility = Visibility.Visible;
            otherTasksMenu.IsOpen = true;

            //object dataContext = ((Control) sender).DataContext;
            //otherTasksMenu.ItemsSource = dataContext as IEnumerable<Task>;
            otherTasksMenu.DataContext = ((Control)sender).DataContext;
        }
    }
}