using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static FracDecConversion.MainWindow;

namespace FracDecConversion
{
    [ValueConversion(typeof(double), typeof(double))]
    public class FractionConverter : IValueConverter
    {
        /// <summary>
        /// Converts from decimal to fraction.
        /// </summary>
        /// <param name="value">The value of type double to be converted to a fraction.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rawValue = value.ToString();
            double parsedValue = 0.0;

            if(Double.TryParse(rawValue, out parsedValue))
                parsedValue = Double.Parse(value.ToString());
            else
                return value;

            double maxError;
            double accuracy = 0.000001;

            int sign = Math.Sign(parsedValue);


            if (sign == -1)
            {
                parsedValue = Math.Abs(parsedValue);
            }

            if(sign == 0)
                maxError = accuracy;
            else
                maxError = (parsedValue * accuracy);

            int n = (int)Math.Floor(parsedValue);
            parsedValue -= n;

            if(parsedValue < maxError)
                return new FractionModel(sign * n, 1).ToString();

            if(1 - maxError < parsedValue)
                return new FractionModel(sign * (n + 1), 1).ToString();

            int lowerNumerator = 0;
            int lowerDenominator = 1;
            int upperNumerator = 1;
            int upperDenominator = 1;

            while(true)
            {
                int middleNumerator = lowerNumerator + upperNumerator;
                int middleDenominator = lowerDenominator + upperDenominator;

                if (middleDenominator * (parsedValue + maxError) < middleNumerator)
                {
                    upperNumerator = middleNumerator;
                    upperDenominator = middleDenominator;
                }
                else if (middleNumerator < (parsedValue - maxError) * middleDenominator)
                {
                    lowerNumerator = middleNumerator;
                    lowerDenominator = middleDenominator;
                }
                else
                {
                    return new FractionModel((n * middleDenominator + middleNumerator) * sign, middleDenominator).ToString();
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rawValue = value.ToString();
            double parsedValue = 0.0;

            if(rawValue.Contains("/") && (rawValue.Split("/".ToCharArray())[1].Length >= 1))
            {
                parsedValue = Double.Parse(parsedValue.ToString());
            }
            else
                return value;

            double numerator = 0;
            double denominator = 0;

            if(double.TryParse(rawValue.Split("/".ToCharArray())[0], out numerator))
            {
                if(double.TryParse(rawValue.Split("/".ToCharArray())[1], out denominator))
                {
                    return (numerator / denominator).ToString();
                }
            }
            else
                return 0;

            numerator = int.Parse(rawValue.Split("/".ToCharArray())[0]);
            denominator = int.Parse(rawValue.Split("/".ToCharArray())[1]);

            return (numerator / denominator).ToString();

        }
    }
}
