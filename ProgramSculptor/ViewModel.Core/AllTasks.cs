﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class AllTasks : INotifyPropertyChanged
    {
        private ITaskDetailsViewModel selectedTaskViewModel;
        private IEnumerable<TaskSolutions> tasks;
        private IMessageService messageService;

        public AllTasks()
        {
            Sandbox = new TaskSolutions {MessageService = MessageService};
            SelectedTaskViewModel = Sandbox;
            SelectTaskCommand = new RelayCommand<TaskSolutions>(selected => SelectTask(selected.Task));

            Sandbox.OpenSolution += OnOpenSolution;
        }

        public TaskSolutions Sandbox { get; }

        public IEnumerable<TaskSolutions> Tasks
        {
            get
            {
                if (tasks == null && MessageService != null)
                {
                    GetTasks();
                }
                return tasks;
            }
        }

        public ITaskDetailsViewModel SelectedTaskViewModel
        {
            get { return selectedTaskViewModel; }
            private set
            {
                if (selectedTaskViewModel != null)
                {
                    selectedTaskViewModel.StartNewSolution -= OnStartNewSolution;
                }
                selectedTaskViewModel = value;
                value.StartNewSolution += OnStartNewSolution;
            }
        }

        public ICommand SelectTaskCommand { get; }
        public ISourceShowerService SourceShower { get; set; }

        public IMessageService MessageService
        {
            get { return messageService; }
            set
            {
                messageService = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }


        protected virtual void OnOpenSolution(Solution solution)
        {
            OpenSolution?.Invoke(solution);
        }

        protected virtual void OnStartNewSolution(Task task)
        {
            StartNewSolution?.Invoke(task);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Task> TryGetTasks()
        {
            try
            {
                return Dao.Factory.TaskDao.AllTasks;
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
                return new Task[0];
            }
        }

        private void SelectTask(Task selectedTask)
        {
            SelectTaskToShow(selectedTask);

            foreach (TaskSolutions solutions in Tasks)
            {
                bool isSelectedTask = ReferenceEquals(solutions.Task, selectedTask);
                solutions.IsSelected = isSelectedTask;
            }
        }

        private void SelectTaskToShow(Task selectedTask)
        {
            if (!Task.Sandbox.Equals(selectedTask))
            {
                TaskViewModel newViewModel = new TaskViewModel(selectedTask)
                {
                    SourceShower = SourceShower,
                    MessageService = MessageService
                };
                newViewModel.GoToTask += SelectTask;
                SelectedTaskViewModel = newViewModel;
            }
            else
            {
                SelectedTaskViewModel = Sandbox;
            }
            OnPropertyChanged(nameof(SelectedTaskViewModel));
        }

        private void GetTasks()
        {
            IEnumerable<Task> taskList = TryGetTasks();
            tasks = taskList.Select(task =>
                new TaskSolutions(task) { MessageService = MessageService }).ToList();
            foreach (TaskSolutions task in Tasks)
            {
                task.OpenSolution += OnOpenSolution;
            }
        }
        
        public event Action<Solution> OpenSolution;
        public event Action<Task> StartNewSolution;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}