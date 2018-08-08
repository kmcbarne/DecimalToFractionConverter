using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FracDecConversion
{
    public class FractionModel
    {
        #region Constructor
        public FractionModel(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        #endregion

        #region Properties
        public int Numerator
        {
            get; private set;
        }

        public int Denominator
        {
            get; private set;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            int mixed = 0;

            while(Numerator >= Denominator)
            {
                mixed += 1;
                Numerator -= Denominator;
            }

            if(mixed != 0)
            {
                if(Numerator == 0)
                    return mixed.ToString();
                else
                    return mixed.ToString() + " " + Numerator.ToString() + "/" + Denominator.ToString();
            }
            else
                return Numerator.ToString() + "/" + Denominator.ToString();
        }
        #endregion

        #region Methods
        public FractionModel FloatToFraction(double value, double accuracy)
        {
            if(accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if(sign == -1)
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
                return new FractionModel(sign * n, 1);
            }

            if(1 - maxError < value)
            {
                return new FractionModel(sign * (n + 1), 1);
            }

            int lowerNumerator = 0;
            int lowerDenominator = 1;

            int upperNumerator = 1;
            int upperDenominator = 1;

            while(true)
            {
                int middleNumerator = lowerNumerator + upperNumerator;
                int middleDenominator = lowerDenominator + upperDenominator;

                if(middleDenominator * (value + maxError) < middleNumerator)
                {
                    upperNumerator = middleNumerator;
                    upperDenominator = middleDenominator;
                }
                else if(middleNumerator < (value - maxError) * middleDenominator)
                {
                    lowerNumerator = middleNumerator;
                    lowerDenominator = middleDenominator;
                }
                else
                {
                    return new FractionModel((n * middleDenominator + middleNumerator) * sign, middleDenominator);
                }
            }
        }
        #endregion
    }
}

