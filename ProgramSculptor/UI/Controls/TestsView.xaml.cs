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
    /// Логика взаимодействия для TestsView.xaml
    /// </summary>
    public partial class TestsView : UserControl
    {
        private const int NONE = -1;

        public TestsView()
        {
            InitializeComponent();
        }

        public void SetTests(/*Test Collection*/)
        {
            testDescription.Text = "No test selected.";
            testSelect.SelectedIndex = NONE;
        }

        private void ViewCode(object sender, MouseButtonEventArgs e)
        {
            // TODO: Call some panel with code.
        }
    }
}
