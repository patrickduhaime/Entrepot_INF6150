﻿using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace EntrepotScreen.Tools
{
    public class SelectedItemToItemsSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return new List<object>() { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}