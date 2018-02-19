using System.ComponentModel;
using System.Windows;
using Model;
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

                navigation = new SolutionNavigation(Solution);
                navigation.PropertyChanged += Navigate;
                DataContext = navigation;

                panels[0] = new TaskSummary();
                panels[1] = new CodeArea();
                panels[2] = new WorkingPanel(); // TOdO: Redo. 
                panels[3] = new WorkingPanel(); // TODO: Redo.

                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].DataContext = navigation.Contexts[i];
                }
            }
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