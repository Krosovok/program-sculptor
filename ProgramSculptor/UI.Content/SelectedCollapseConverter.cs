﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Content
{
    public class SelectedCollapseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool selected = (bool) value;
            return selected ? 
                Visibility.Visible :
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
