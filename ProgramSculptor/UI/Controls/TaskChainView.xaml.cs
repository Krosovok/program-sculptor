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
    /// Логика взаимодействия для TaskChainView.xaml
    /// </summary>
    public partial class TaskChainView : UserControl
    {
        private readonly ContextMenu otherTaskMenu;

        public TaskChainView()
        {
            InitializeComponent();

            otherTaskMenu = (ContextMenu) Resources["OtherTaskMenu"];
        }

        private void ContextMenuClick(object sender, RoutedEventArgs e)
        {
            otherTaskMenu.Visibility = Visibility.Visible;
            otherTaskMenu.IsOpen = true;
        }
    }
}