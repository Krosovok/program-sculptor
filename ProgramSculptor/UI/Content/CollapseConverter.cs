using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ViewModel;

namespace UI.Content
{
    public class CollapseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TaskChainPosition chain = value as TaskChainPosition;

            return HasChainNotFromOne(chain) ?
                Visibility.Visible :
                Visibility.Collapsed;
        }

        private static bool HasChainNotFromOne(TaskChainPosition chain)
        {
            return chain != null && chain.HasChain;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
