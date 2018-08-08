using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FracDecConversion
{
    public class VolumeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.0;
            double originVolume = 0.0;
            double literVolume = 0.0;

            //Currently implemented units:
            //"in³", "ft³", "yd³", "mm³", "cm³", "cc", "m³", "mL", "L", "gal", "qt", "pt", "fl oz"

            if(values[1] != null && values[2] != null)
            {
                if (double.TryParse(values[0].ToString(), out originVolume))
                {
                    //First convert the volume to a standard conversion unit of liters...
                    literVolume = ConvertToLiter(values[1].ToString(), originVolume);
                    //Then convert from liters to the desired destination unit
                    result = ConvertFromLiter(values[2].ToString(), literVolume);
                }
            }
            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }

        #region Conversion Methods
        public double ConvertToLiter(string originUnit, double originVolume)
        {
            double result = 0.0;

            switch(originUnit)
            {
                //"in³", "ft³", "yd³", "mm³", "cm³", "cc", "m³", "mL", "L", "gal", "qt", "pt", "fl oz"
                case "in³":
                    result = originVolume * 0.01638706;
                    break;
                case "ft³":
                    result = originVolume * 28.3168466;
                    break;
                case "yd³":
                    result = originVolume * 764.554858;
                    break;
                case "mm³":
                    result = originVolume * 0.000001;
                    break;
                case "cm³":
                    result = originVolume * 0.001;
                    break;
                case "cc":
                    result = originVolume * 0.001;
                    break;
                case "m³":
                    result = originVolume * 1000;
                    break;
                case "mL":
                    result = originVolume * 0.001;
                    break;
                case "L":
                    //No conversion required
                    result = originVolume;
                    break;
                case "gal":
                    //1 gal = 3.78541178 L
                    result = originVolume * 3.78541178;
                    break;
                case "qt":
                    //1 qt = 0.94635295 L
                    result = originVolume * 0.94635295;
                    break;
                case "pt":
                    //1 pt = 0.47317647 L
                    result = originVolume * 0.47317647;
                    break;
                case "fl oz":
                    //1 fl oz = 0.02957353 L
                    result = originVolume * 0.02957353;
                    break;
            }
            return result;
        }

        public double ConvertFromLiter(string destinationUnit, double literVolume)
        {
            double result = 0.0;

            switch(destinationUnit)
            {
                //"in³", "ft³", "yd³", "mm³", "cm³", "cc", "m³", "mL", "L", "gal", "qt", "pt", "fl oz"
                case "in³":
                    //1 L = 61.0237441 in³
                    result = literVolume * 61.0237441;
                    break;
                case "ft³":
                    //1 L = 0.03531467 ft³
                    result = literVolume * 0.03531467;
                    break;
                case "yd³":
                    //1 L = 0.00130795 yd³
                    result = literVolume * 0.00130795;
                    break;
                case "mm³":
                    //1 L = 1000000 mm³
                    result = literVolume * 1000000;
                    break;
                case "cm³":
                    //1 L = 1000 cm³
                    result = literVolume * 1000;
                    break;
                case "cc":
                    //1 L = 1000 cc
                    result = literVolume * 1000;
                    break;
                case "m³":
                    //1 L = 0.001 m³
                    result = literVolume * 0.001;
                    break;
                case "mL":
                    //1 L = 1000 mL
                    result = literVolume * 1000;
                    break;
                case "L":
                    //No conversion required
                    result = literVolume;
                    break;
                case "gal":
                    //1 L = 0.2642 gal
                    result = literVolume * 0.2642;
                    break;
                case "qt":
                    //1 L = 1.0567 qt
                    result = literVolume * 1.0567;
                    break;
                case "pt":
                    //1 L = 2.1134 pt
                    result = literVolume * 2.1134;
                    break;
                case "fl oz":
                    //1 L = 33.8140227 fl oz
                    result = literVolume * 33.8140227;
                    break;
            }
            return result;
        }
        #endregion
    }
}
