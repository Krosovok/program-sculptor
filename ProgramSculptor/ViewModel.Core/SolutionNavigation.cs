using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using ProgramSculptor.Initialization;
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

        private readonly IWorkflowStep[] panelDataContexts = new IWorkflowStep[4];
        private readonly IMessageService messageService;
        private readonly IDialogFactory dialogFactory;
        private int panelIndex;

        public SolutionNavigation(Solution solution,
            WindowContentNavigator parent)
        {
            this.messageService = parent.MessageService;
            this.dialogFactory = parent.DialogFactory;
            HomeCommand = parent.HomeCommand;

            InitContexts(solution);

            ToLeftPanelCommand = new RelayCommand<object>(
                MoveToLeft,
                o => PanelIndex > 0);
            ToRightPanelCommand = new RelayCommand<object>(
                MoveToRight,
                o => PanelIndex < panelDataContexts.Length - 1);
        }

        public IWorkflowStep Current => panelDataContexts[PanelIndex];
        public ICommand ToLeftPanelCommand { get; }
        public ICommand ToRightPanelCommand { get; }

        public ICommand HomeCommand { get; }

        //public LoadedClasses LoadedClasses { get; set; }
        //public ModelInitialization ModelInitialization { get; set; }
        public ModelRunner ModelRunner { get; private set; }

        private int PanelIndex
        {
            get { return panelIndex; }
            set
            {
                panelIndex = value;
                OnPropertyChanged(nameof(Current));
            }
        }

        private IWorkflowStep Previous => panelDataContexts[PanelIndex - 1];

        private void InitContexts(Solution solution)
        {
            LoadedClasses loadedClasses = new LoadedClasses(solution,
                messageService,
                dialogFactory);
            ModelInitialization modelInitialization = new ModelInitialization(
                loadedClasses, 
                messageService);
            ModelRunner = new ModelRunner();
            ModelRunner.UserCodeException += UserCodeException;

            panelDataContexts[TaskSummaryStep] = new TaskViewModel(solution.Task);
            panelDataContexts[EditCodeStep] = loadedClasses;
            panelDataContexts[ModelInitializationStep] = modelInitialization;
            panelDataContexts[ModelRunningStep] = ModelRunner;
        }

        private void UserCodeException(Exception userError)
        {
            string message =
                "An exception occured in your code. " + Environment.NewLine +
                userError;
            messageService.Show(message);
        }

        private void MoveToLeft(object o)
        {
            PanelIndex--;
            Current.Clear();
        }

        private void MoveToRight(object obj)
        {
            PanelIndex++;
            Update();
            // TODO: Save model settings somwhere not to reinput them each time?
        }

        private void Update()
        {
            try
            {
                Current.Update(Previous);
            }
            catch (WorkflowException e)
            {
                GoBack(e.Message);
            }
        }
        
        private void GoBack(string message)
        {
            messageService.Show(message);
            PanelIndex--;
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}