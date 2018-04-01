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
        private SolutionNavigation Navigation => DataContext as SolutionNavigation;
        
        public SolutionContent()
        {
            InitializeComponent();
        }
        
        //private void InitPanels()
        //{
        //    Binding binding = new Binding(nameof(ModelRunner.Model))
        //    {
        //        Source = Navigation.ModelRunner
        //    };
        //    Field.SetBinding(DataContextProperty, binding);
        //}

        //private void Navigate(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        //{
        //    if (NotCurrent(propertyChangedEventArgs))
        //    {
        //        return;
        //    }

        //    SolutionNavigation solutionNavigation = (SolutionNavigation)sender;
        //    ShownPanel = panels[solutionNavigation.PanelIndex];
        //}

        //private static bool NotCurrent(PropertyChangedEventArgs propertyChangedEventArgs)
        //{
        //    return !propertyChangedEventArgs.PropertyName
        //        .Equals(nameof(SolutionNavigation.Current));
        //}

        //private void SolutionChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(DataContext is SolutionNavigation))
        //        return;
            
        //    InitPanels();
        //    ShownPanel = panels[0];
        //    Navigation.PropertyChanged += Navigate;
        //}
    }
}
