using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class SolutionNavigation : INotifyPropertyChanged
    {
        private readonly object[] PanelDataContexts = new object[4];
        private int panelIndex;

        public SolutionNavigation(Solution solution)
        {
            PanelDataContexts[0] = solution.Task;
            PanelDataContexts[1] = new LoadedClasses(solution);
            PanelDataContexts[2] = new NotImplementedException();
            PanelDataContexts[3] = new NotImplementedException();

            ToLeftPanelCommand = new RelayCommand<object>(
                o => PanelIndex--//,
                //o => PanelIndex > 0);
                //o => true);
            );
            ToRightPanelCommand = new RelayCommand<object>(
                o => PanelIndex++//,
                //o => PanelIndex < PanelDataContexts.Length - 1);
                //o => true);
            );
        }

        public int PanelIndex
        {
            get { return panelIndex; }
            private set
            {
                panelIndex = value; 
                OnPropertyChanged(nameof(PanelIndex));
            }
        }

        public IReadOnlyList<object> Contexts => PanelDataContexts;

        public ICommand ToLeftPanelCommand { get; }
        public ICommand ToRightPanelCommand { get; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}