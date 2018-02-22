using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using ProgramSculptor.Initialization;
using ProgramSculptor.Running;
using Services;

namespace ViewModel
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

            InitContexts(solution, messageService, dialogFactory);

            ToLeftPanelCommand = new RelayCommand<object>(
                o => PanelIndex--,
                o => PanelIndex > 0);
            ToRightPanelCommand = new RelayCommand<object>(
                o =>
                {
                    PanelIndex++;
                    Update(); // TODO: Check to update only if there are enough classes.
                },
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

        private void InitContexts(Solution solution, IMessageService messageService, IDialogFactory dialogFactory)
        {
            LoadedClasses classes = new LoadedClasses(solution,
                messageService,
                dialogFactory);
            ModelInitialization initialization = new ModelInitialization(classes);

            panelDataContexts[TaskSummaryStep] = solution.Task;
            panelDataContexts[EditCodeStep] = classes;
            panelDataContexts[ModelInitializationStep] = initialization;
            panelDataContexts[ModelRunningStep] = new ModelRunner();
        }

        private void Update()
        {
            if (PanelIndex != ModelInitializationStep) return;

            switch (PanelIndex)
            {
                case ModelInitializationStep:
                    TryCompileClasses();
                    break;

                case ModelRunningStep:
                    // TODO: Update model.
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
            ((ModelInitialization) panelDataContexts[ModelInitializationStep])
                .Update(
                    (LoadedClasses) panelDataContexts[EditCodeStep]);
        }

        public IReadOnlyList<object> Contexts => panelDataContexts;

        public ICommand ToLeftPanelCommand { get; }
        public ICommand ToRightPanelCommand { get; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}