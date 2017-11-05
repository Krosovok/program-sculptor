using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для SandboxPanel.xaml
    /// </summary>
    public partial class SandboxPanel : UserControl
    {
        public SandboxPanel()
        {
            InitializeComponent();

            //(Application.Current as App).LanguageChanged += ChangeLanguage;
            //// TODO: Add I18n support.
        }

        //private void ChangeLanguage(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void NewSandbox(object sender, RoutedEventArgs e)
        {
            // TODO: Start new sandbox here.
        }

        private void ProjectSelected(object sender, SelectionChangedEventArgs e)
        {
            // Extaract data from project description.
            object selected = e.AddedItems[0];
            projectInfo.Document = new FlowDocument(
                new Paragraph(
                    new Run(selected.ToString())
                )
            );
        }
    }
}
