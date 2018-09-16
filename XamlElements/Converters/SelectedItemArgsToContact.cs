using Models.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XamlElements.Converters
{
    public class SelectedItemArgsToContact : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SelectedItemChangedEventArgs val = (SelectedItemChangedEventArgs)value;
            return (Contact)val.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
