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

        public AllTasks()
        {
            IReadOnlyList<Task> tasks = Dao.Factory.TaskDao.AllTasks;
            Tasks = tasks.Select(task => new TaskSolutions(task)).ToList();
            SelectedTaskViewModel = Sandbox;
            SelectTaskCommand = new RelayCommand<TaskSolutions>(selected => SelectTask(selected.Task));

            foreach (TaskSolutions task in Tasks)
            {
                task.OpenSolution += OnOpenSolution;
            }
            Sandbox.OpenSolution += OnOpenSolution;
        }
        
        public TaskSolutions Sandbox { get; } = new TaskSolutions();
        public IEnumerable<TaskSolutions> Tasks { get; }
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
                TaskViewModel newViewModel = new TaskViewModel(selectedTask) {SourceShower = SourceShower};
                newViewModel.GoToTask += SelectTask;
                SelectedTaskViewModel = newViewModel;
            }
            else
            {
                SelectedTaskViewModel = Sandbox;
            }
            OnPropertyChanged(nameof(SelectedTaskViewModel));
        }

        public event Action<Solution> OpenSolution;
        public event Action<Task> StartNewSolution;
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
