using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Model;
using Services.Dialog;
using Services.Message;
using UI.Controls;
using ViewModel;

namespace UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private readonly WorkingPanel[] panels = new WorkingPanel[4];
        private SolutionNavigation navigation;
        private Solution solution;

        internal TaskWindow()
        {
            DataContext = navigation;
            InitializeComponent();
        }

        public TaskWindow(Solution solution) : this()
        {
            Solution = solution;
            ShownPanel = panels[0];
        }

        public Solution Solution
        {
            get { return solution; }
            set
            {
                solution = value;

                navigation = new SolutionNavigation(Solution, new MessageBoxService(), new DialogFactory());
                navigation.PropertyChanged += Navigate;
                DataContext = navigation;

                InitPanels();
            }
        }

        private void InitPanels()
        {
            panels[0] = new TaskSummary();
            panels[1] = new CodeArea();
            panels[2] = new ModelSettings();
            panels[3] = new WorkingPanel(); // TODO: Redo.

            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].DataContext = navigation.Contexts[i];
            }

            Binding binding = new Binding(nameof(ModelRunner.Model))
            {
                Source = navigation.ModelRunner
            };
            Field.SetBinding(DataContextProperty, binding);
        }

        private WorkingPanel ShownPanel
        {
            set { WorkingPanel.Navigate(value); }
        }

        private void Navigate(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (NotPanelIndex(propertyChangedEventArgs))
            {
                return;
            }

            SolutionNavigation solutionNavigation = (SolutionNavigation) sender;
            ShownPanel = panels[solutionNavigation.PanelIndex];
        }

        private static bool NotPanelIndex(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            return !propertyChangedEventArgs.PropertyName
                .Equals(nameof(SolutionNavigation.PanelIndex));
        }
    }
}