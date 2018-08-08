using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace FracDecConversion
{
    public class MeasurementModel
    {
        #region Constructor
        public MeasurementModel()
        {

        }
        #endregion

        #region Members

        #endregion

        #region Properties

        /*
        ***************************
        *                         *
        *    Not the one in use   *
        *                         *
        ***************************
        */
        public IList<string> Units
        {
            get { return new List<string>() { "cm", "mm", "in", "ft", "yd" }; }
        }
        #endregion

        #region Methods

        #endregion
    }
}
