using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для LanguageSwitch.xaml
    /// </summary>
    public partial class LanguageSwitch : UserControl
    {
        readonly App application = (App.Current as App);

        public LanguageSwitch()
        {
            InitializeComponent();
            SetLanguages();
        }

        private void SetLanguages()
        {
            foreach (CultureInfo language in application.Languages)
            {
                languages.Items.Add(language);
            }
            languages.SelectedItem = application.Language;
        }

        private void LanguageSelected(object sender, RoutedEventArgs e)
        {
            ComboBox senderComboBox = sender as ComboBox;
            application.Language = senderComboBox.SelectedItem as CultureInfo;
        }
    }
}
