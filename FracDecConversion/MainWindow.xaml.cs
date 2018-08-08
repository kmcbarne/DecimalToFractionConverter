using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FracDecConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //decimalEntry:  TextBox with entered number
        //fractionResult:  TextBox to display results
        //convert:  Button to execute conversion

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MeasurementViewModel();
            //this.DataContext = new VolumeViewModel();
        }

        public Fraction FloatToFraction (double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            //double maxError = sign == 0 ? accuracy : value * accuracy;

            //Possibly the standard if-then statement, need to TEST
            double maxError;
            if(sign == 0)
                maxError = accuracy;
            else
                maxError = (value * accuracy);

            int n = (int)Math.Floor(value);
            value -= n;

            if(value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            int lowerNumerator = 0;
            int lowerDenominator = 1;

            int upperNumerator = 1;
            int upperDenominator = 1;

            while(true)
            {
                int middleNumerator = lowerNumerator + upperNumerator;
                int middleDenominator = lowerDenominator + upperDenominator;

                if (middleDenominator * (value + maxError) < middleNumerator)
                {
                    upperNumerator = middleNumerator;
                    upperDenominator = middleDenominator;
                }
                else if (middleNumerator < (value - maxError) * middleDenominator)
                {
                    lowerNumerator = middleNumerator;
                    lowerDenominator = middleDenominator;
                }
                else
                {
                    return new Fraction((n * middleDenominator + middleNumerator) * sign, middleDenominator);
                }
            }
        }

        public class Fraction
        {
            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }

            public int Numerator
            {
                get; private set;
            }

            public int Denominator
            {
                get; private set;
            }
            
            public override string ToString()
            {
                int mixed = 0;

                while (Numerator >= Denominator)
                {
                    mixed += 1;
                    Numerator -= Denominator;
                }

                if (mixed != 0)
                {
                    if(Numerator == 0)
                        return mixed.ToString();
                    else
                        return mixed.ToString() + " " + Numerator.ToString() + "/" + Denominator.ToString();
                }                    
                else
                    return Numerator.ToString() + "/" + Denominator.ToString();
            }
        }

        private void convertFraction_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(decimalEntry.Text == "")
                {
                    throw new ArgumentOutOfRangeException("decimalEntry.Text", "A decimal number needs to be entered in the decimal box.");
                }

                fractionResult.Text = "";

                double dec = Double.Parse(decimalEntry.Text);
                double accuracy = 0.000001;

                fractionResult.Text = FloatToFraction(dec, accuracy).ToString();
            }
            catch(FormatException fex)
            {
                systemErrorMessages.Content = fex.Message.ToString();
                
            }
            catch(ArgumentOutOfRangeException aorex)
            {
                systemErrorMessages.Content = aorex.Message.ToString();
            }
            catch(Exception ex)
            {
                systemErrorMessages.Content = ex.Message.ToString();
            }
            
        }

        private void decimalEntry_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Enter)
            {
                convertFraction_Click(null, null);
            }
        }

        private void convertLength_Click(object sender, RoutedEventArgs e)
        {

        }        
    }
}
