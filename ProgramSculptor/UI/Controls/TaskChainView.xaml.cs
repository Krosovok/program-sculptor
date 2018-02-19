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
    public partial class TaskChainView : UserControl
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
            // TODO: Change to show task names. Do something to the Command...
            otherTasksMenu = (ContextMenu) Resources["OtherTaskMenu"];
        }

        public ICommand ChangeTaskCommand => new RelayCommand<Task>(ChangeSelectedTask);

        private void ContextMenuOpenClick(object sender, RoutedEventArgs e)
        {
            otherTasksMenu.Visibility = Visibility.Visible;
            otherTasksMenu.IsOpen = true;

            object dataContext = ((Control) sender).DataContext;
            otherTasksMenu.ItemsSource = dataContext as IEnumerable<Task>;
        }

        private void TaskChainChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
            {
                ButtonGrid.Visibility = Visibility.Hidden;
                Display.Document = noChainDisplay;
            }
            else if (DataContext.GetType() == typeof(TaskChainPosition))
            {
                SetTaskChain();
            }
        }

        private void SetTaskChain()
        {
            TaskChainPosition chain = (TaskChainPosition) DataContext;

            AllBefore.DataContext = chain.AllBefore;
            Previous.DataContext = chain.Previous;
            Current.DataContext = chain.Current;
            Next.DataContext = chain.Next;
            AllAfter.DataContext = chain.AllAfter;

            ButtonGrid.Visibility = Visibility.Visible;
            SetVisibilityToButtons();

            // TODO: Set FlowDocument show details on selected task in chain.
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
            button.Visibility = button.DataContext == null ? Visibility.Hidden : Visibility.Visible;
        }

        private void ChangeSelectedTask(Task newTask)
        {
            //DataContext = newTask.InChain;
        }
    }
}