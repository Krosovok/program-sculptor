using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ||
                   CheckCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute((T) parameter);
        }

        private bool CheckCanExecute(object parameter)
        {
            if (parameter == null || parameter is T)
            {
                return canExecute((T) parameter);
            }

            return false;
        }
    }
}