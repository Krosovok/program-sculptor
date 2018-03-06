using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using ViewModel.Core;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для SolutionContent.xaml
    /// </summary>
    public partial class SolutionContent
    {
        private readonly WorkingPanel[] panels = new WorkingPanel[4];
        private SolutionNavigation Navigation => DataContext as SolutionNavigation;
        
        public SolutionContent()
        {
            InitializeComponent();
        }
        
        private WorkingPanel ShownPanel
        {
            set { WorkingPanel.Navigate(value); }
        }

        private void InitPanels()
        {
            panels[0] = new TaskSummary();
            panels[1] = new CodeArea();
            panels[2] = new ModelSettings();
            panels[3] = new ModelControlPanel();

            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].DataContext = Navigation.Contexts[i];
            }

            Binding binding = new Binding(nameof(ModelRunner.Model))
            {
                Source = Navigation.ModelRunner
            };
            Field.SetBinding(DataContextProperty, binding);
        }

        private void Navigate(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (NotPanelIndex(propertyChangedEventArgs))
            {
                return;
            }

            SolutionNavigation solutionNavigation = (SolutionNavigation)sender;
            ShownPanel = panels[solutionNavigation.PanelIndex];
        }

        private static bool NotPanelIndex(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            return !propertyChangedEventArgs.PropertyName
                .Equals(nameof(SolutionNavigation.PanelIndex));
        }

        private void SolutionChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InitPanels();
            ShownPanel = panels[0];
            Navigation.PropertyChanged += Navigate;
        }
    }
}
