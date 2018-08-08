using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FracDecConversion
{
    [ValueConversion(typeof(double), typeof(double))]
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sourceValue = value.ToString();
            double decimalValue = 0.0;

            if(Double.TryParse(sourceValue, out decimalValue))
            {
                return Math.Round((decimalValue - 32.0) * (5.0 / 9.0), 1);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sourceValue = value.ToString();
            double decimalValue = 0.0;

            if(Double.TryParse(sourceValue, out decimalValue))
            {
                return Math.Round((decimalValue * (9.0 / 5.0)) + 32.0, 1);
            }
            return value;
        }
    }
}
