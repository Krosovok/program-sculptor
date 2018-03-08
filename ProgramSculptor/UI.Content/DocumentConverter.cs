using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Content
{
    public class DocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DocumentBuilder builder = new DocumentBuilder();
            builder.AddContent(value?.ToString());
            return builder.Document;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
