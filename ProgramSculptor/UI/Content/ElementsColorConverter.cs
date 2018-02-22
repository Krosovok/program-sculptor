using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel;

namespace UI.Content
{
    public class ElementsColorConverter : IValueConverter
    {
        public ModelRunner Runner { set; get; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type elementType = value.GetType();
            return Runner.TypeColor(elementType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
