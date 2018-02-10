﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using UI.Controls.Events;
using Task = Model.Task;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskPanel.xaml
    /// </summary>
    public partial class TaskPanel : UserControl
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

        public Task SelectedTask { get; }

        public event RoutedEventHandler NewSolution
        {
            add { AddHandler(NewSolutionEvent, value); }
            remove { RemoveHandler(NewSolutionEvent, value); }
        }

        public TaskPanel(Task selectedTask)
        {
            SelectedTask = selectedTask;
            DataContext = selectedTask;
            InitializeComponent();
        }

        private void StartNewSolution(object sender, RoutedEventArgs e)
        {
            DockPanel content = (DockPanel) Resources[SolutionNameInputKey];
            NewSolutionActionFrame.Navigate(content);
        }

        private void CreateSolution(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter))
            {
                return;
            }

            AddNewSolution(sender);
        }

        private void AddNewSolution(object sender)
        {
            Solution newSolution = BuildSolution(sender);

            Dao.Factory.SolutionDao.AddSolution(newSolution);
            RaiseEvent(new RoutedEventArgs(NewSolutionEvent, this));
        }

        private Solution BuildSolution(object sender)
        {
            TextBox textBox = (TextBox) sender;
            string solutionName = string.IsNullOrEmpty(textBox.Text) ? null : textBox.Text;
            Solution newSolution = new Solution(solutionName, Dao.Factory.UserDao.CurrentUser, SelectedTask);
            return newSolution;
        }

        /*
         TODO: this:
         3. Выполнить функцию, отобразив результат в приложении.

4. Обработать исключительную ситуацию, сгенерированную серверной частью приложения 
(желательно инициировать пользовательское исключение и обработать его в клиентском приложении).
         */
    }
}