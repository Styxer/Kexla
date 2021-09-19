using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Kexla.Samples.Convertors
{
    public class NumberToIconCovert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == DBNull.Value)
            {
                return DependencyProperty.UnsetValue;
            }
            var input = System.Convert.ToInt16(value);
            
            switch(input)
            {
                case 1:
                    return MaterialDesignThemes.Wpf.PackIconKind.Alarm;
                case 2:
                    return MaterialDesignThemes.Wpf.PackIconKind.Airport;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
