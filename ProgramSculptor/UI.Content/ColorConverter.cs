using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Content
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            
            Color color = (Color) value;

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            
            return brush?.Color;
        }
    }
}
