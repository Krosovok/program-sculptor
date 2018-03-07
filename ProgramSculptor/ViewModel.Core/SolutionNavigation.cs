using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using ProgramSculptor.Initialization;
using ProgramSculptor.Running;
using Services;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class SolutionNavigation : INotifyPropertyChanged
    {
        private const int TaskSummaryStep = 0;
        private const int EditCodeStep = 1;
        private const int ModelInitializationStep = 2;
        private const int ModelRunningStep = 3;

        private readonly object[] panelDataContexts = new object[4];
        private readonly IMessageService messageService;
        private readonly IDialogFactory dialogFactory;
        private int panelIndex;

        public SolutionNavigation(Solution solution,
            IMessageService messageService,
            IDialogFactory dialogFactory)
        {
            this.messageService = messageService;
            this.dialogFactory = dialogFactory;

            InitContexts(solution);

            ToLeftPanelCommand = new RelayCommand<object>(
                MoveToLeft,
                o => PanelIndex > 0);
            ToRightPanelCommand = new RelayCommand<object>(
                MoveToRight,
                o => PanelIndex < panelDataContexts.Length - 1);
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
        public IReadOnlyList<object> Contexts => panelDataContexts;
        public ICommand ToLeftPanelCommand { get; }
        public ICommand ToRightPanelCommand { get; }
        public LoadedClasses LoadedClasses { get; set; }
        public ModelInitialization ModelInitialization { get; set; }
        public ModelRunner ModelRunner { get; set; }

        private void MoveToLeft(object o)
        {
            if (PanelIndex == ModelRunningStep)
            {
                ModelRunner.Clear();
            }
            PanelIndex--;
        }
        
        private void MoveToRight(object obj)
        {
            PanelIndex++;
            Update(); // TODO: Check to update only if there are enough classes.
        }

        private void InitContexts(Solution solution)
        {
            LoadedClasses = new LoadedClasses(solution,
                messageService,
                dialogFactory);
            ModelInitialization = new ModelInitialization(LoadedClasses);
            ModelRunner = new ModelRunner();

            panelDataContexts[TaskSummaryStep] = solution.Task;
            panelDataContexts[EditCodeStep] = LoadedClasses;
            panelDataContexts[ModelInitializationStep] = ModelInitialization;
            panelDataContexts[ModelRunningStep] = ModelRunner;
        }

        private void Update()
        {
            switch (PanelIndex)
            {
                case ModelInitializationStep:
                    TryCompileClasses();
                    break;

                case ModelRunningStep:
                    ModelRunner.Update(ModelInitialization);
                    break;
            }
        }

        private void TryCompileClasses()
        {
            try
            {
                CompileClasses();
            }
            catch (InitializationException e)
            {
                messageService.Show(e.Message);
                PanelIndex--;
            }
            catch (CodeException e)
            {
                messageService.Show(e.Message);
                PanelIndex--;
            }
        }
        
        private void CompileClasses()
        {
            ModelInitialization
                .Update(LoadedClasses);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}