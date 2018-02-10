using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Model;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskChainView.xaml
    /// </summary>
    public partial class TaskChainView : UserControl
    {
        private readonly ContextMenu otherTasksMenu;

        public TaskChainView()
        {
            InitializeComponent();

            otherTasksMenu = (ContextMenu) Resources["OtherTaskMenu"];
        }

        private void ContextMenuOpenClick(object sender, RoutedEventArgs e)
        {
            otherTasksMenu.Visibility = Visibility.Visible;
            otherTasksMenu.IsOpen = true;

            object dataContext = ((Control) sender).DataContext;
            otherTasksMenu.ItemsSource = dataContext as IEnumerable<Task>;
            
            // TODO: Menu items click.
        }

        private void TaskChainChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TaskChainPosition chain = (TaskChainPosition) DataContext;

            AllBefore.DataContext = chain.AllBefore;
            Previous.DataContext = chain.Previous;
            Current.DataContext = chain.Current;
            Next.DataContext = chain.Next;
            AllAfter.DataContext = chain.AllAfter;

            SetVisibilityToButtons();
        }

        private void SetVisibilityToButtons()
        {
            foreach (Button taskLinkButton in ButtonGrid.Children.OfType<Button>())
            {
                SetVisibility(taskLinkButton);
            }
        }

        private static void SetVisibility(Button button)
        {
            button.Visibility = button.DataContext == null ? 
                Visibility.Hidden :
                Visibility.Visible;
        }
    }
}