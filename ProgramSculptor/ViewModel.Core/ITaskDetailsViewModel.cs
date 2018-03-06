using System;
using System.Windows.Input;
using Model;

namespace ViewModel.Core
{
    public interface ITaskDetailsViewModel
    {
        Task Task { get; }
        ICommand StartNewSolutionCommand { get; }

        event Action<Task> StartNewSolution;
    }
}
