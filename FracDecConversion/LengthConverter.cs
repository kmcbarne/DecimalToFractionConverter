using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FracDecConversion
{
    public class LengthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.0;
            double originLength = 0.0;
            double meterLength = 0.0;

            #region Initial Parameters
            //double cm_to_mm = 10;
            //double cm_to_in = 0.3937;
            //double cm_to_ft = 0.32808;
            //double cm_to_yd = 0.010936;
            //double mm_to_cm = 0.10;
            //double mm_to_in = mm_to_cm * cm_to_in;
            //double mm_to_ft = mm_to_cm * cm_to_ft;
            //double mm_to_yd = mm_to_cm * cm_to_yd;
            //double in_to_mm = 25.4;
            //double in_to_cm = 2.54;
            //double in_to_ft = 0.0833333333333333;
            //double in_to_yd = 0.0277777777777778;
            //double ft_to_yd = 0.3333333333333333;
            //double ft_to_in = 12;
            //double ft_to_cm = ft_to_in * in_to_cm;
            //double ft_to_mm = ft_to_in * in_to_mm;
            //double yd_to_ft = 3;
            //double yd_to_in = 36;
            //double yd_to_cm = yd_to_in * in_to_cm;
            //double yd_to_mm = yd_to_in * in_to_mm;
            #endregion

            //Currently implemented units:
            //"mm", "cm", "m", "km", "in", "ft", "yd", "mi"

            if(values[1] != null && values[2] != null)
            {
                if(double.TryParse(values[0].ToString(), out originLength))
                {
                    //First convert the length to a standard conversion unit of meters...
                    meterLength = ConvertToMeter(values[1].ToString(), originLength);
                    //Then convert from meters to the desired destination unit
                    result = ConvertFromMeter(values[2].ToString(), meterLength);
                }

                #region Initial Approach
                //switch(values[1].ToString())
                //{
                //    //Completed
                //    case "mm":
                //        if (double.TryParse(values[0].ToString(), out result))
                //        {
                //            switch(values[2].ToString())
                //            {
                //                case "cm":
                //                    result = result * mm_to_cm;
                //                    break;
                //                case "in":
                //                    result = result * mm_to_in;
                //                    break;
                //                case "ft":
                //                    result = result * mm_to_ft;
                //                    break;
                //                case "yd":
                //                    result = result * mm_to_yd;
                //                    break;
                //            }
                //        }
                //        break;
                //    //Completed
                //    case "cm":
                //        if(double.TryParse(values[0].ToString(), out result))
                //        {
                //            switch(values[2].ToString())
                //            {
                //                //Completed
                //                case "mm":
                //                    result = result * cm_to_mm;
                //                    break;
                //                //Completed
                //                case "in":
                //                    result = result * cm_to_in;
                //                    break;
                //                //Completed
                //                case "ft":
                //                    result = result * cm_to_ft;
                //                    break;
                //                //Completed
                //                case "yd":
                //                    result = result * cm_to_yd;
                //                    break;
                //            }
                //        }
                //        break;
                //    //Completed
                //    case "in":
                //        if(double.TryParse(values[0].ToString(), out result))
                //        {
                //            switch(values[2].ToString())
                //            {
                //                case "mm":
                //                    result = result * in_to_mm;
                //                    break;
                //                case "cm":
                //                    result = result * in_to_cm;
                //                    break;
                //                case "ft":
                //                    result = result * in_to_ft;
                //                    break;
                //                case "yd":
                //                    result = result * in_to_yd;
                //                    break;
                //            }
                //        }
                //        break;
                //    //Completed
                //    case "ft":
                //        if(double.TryParse(values[0].ToString(), out result))
                //        {
                //            switch(values[2].ToString())
                //            {
                //                case "mm":
                //                    result = result * ft_to_mm;
                //                    break;
                //                case "cm":
                //                    result = result * ft_to_cm;
                //                    break;
                //                case "in":
                //                    result = result * ft_to_in;
                //                    break;
                //                case "yd":
                //                    result = result * ft_to_yd;
                //                    break;
                //            }
                //        }
                //        break;
                //    case "yd":
                //        if(double.TryParse(values[0].ToString(), out result))
                //        {
                //            switch(values[2].ToString())
                //            {
                //                case "mm":
                //                    result = result * yd_to_mm;
                //                    break;
                //                case "cm":
                //                    result = result * yd_to_cm;
                //                    break;
                //                case "in":
                //                    result = result * yd_to_in;
                //                    break;
                //                case "ft":
                //                    result = result * yd_to_ft;
                //                    break;
                //            }
                //        }
                //        break;
                //}
                #endregion
            }
            return result.ToString();            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        /// <summary>
        /// Converts a length from the source unit to a standard conversion unit (meters).
        /// </summary>
        /// <param name="originUnit">The unit of measurment of the original length.</param>
        /// <param name="originLength">The numeric value of the original length.</param>
        /// <returns></returns>
        public double ConvertToMeter(string originUnit, double originLength)
        {
            double result = 0.0;

            switch(originUnit)
            {
                case "mm":
                    //1 mm = 0.001 m
                    result = originLength / 1000;
                    break;
                case "cm":
                    //1 cm = 0.01 cm
                    result = originLength / 100;
                    break;
                case "m":
                    //No conversion required
                    result = originLength;
                    break;
                case "km":
                    //1 km = 1000 m
                    result = originLength * 1000;
                    break;
                case "in":
                    //1 in = 0.0254 m
                    result = originLength * 0.0254;
                    break;
                case "ft":
                    //1 ft = 0.3048 m
                    result = originLength * 0.3048;
                    break;
                case "yd":
                    //1 yd = 0.9144 m
                    result = originLength * 0.9144;
                    break;
                case "mi":
                    //1 mi = 1609.3 m
                    result = originLength * 1609.3;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Converts a length from the standard conversion unit (meters) to the desired destination unit of measure.
        /// </summary>
        /// <param name="destinationUnit">The unit of measurement that the length will be converted to.</param>
        /// <param name="meterLength">The numeric value of the length in meters.</param>
        /// <returns></returns>
        public double ConvertFromMeter(string destinationUnit, double meterLength)
        {
            double result = 0.0;

            switch(destinationUnit)
            {
                case "mm":
                    //1 m = 1000 mm
                    result = meterLength * 1000;
                    break;
                case "cm":
                    //1 m = 100 cm
                    result = meterLength * 100;
                    break;
                case "m":
                    //No conversion required
                    result = meterLength;
                    break;
                case "km":
                    //1 m = 0.001 km
                    result = meterLength / 1000;
                    break;
                case "in":
                    //1 m = 39.3696 in
                    result = meterLength * 39.3696;
                    break;
                case "ft":
                    //1 m = 3.2808 ft
                    result = meterLength * 3.2808;
                    break;
                case "yd":
                    //1 m = 1.0936 yd
                    result = meterLength * 1.0936;
                    break;
                case "mi":
                    //1 m = 0.000621 mi
                    result = meterLength * 0.000621;
                    break;
            }
            return result;
        }
    }
}
