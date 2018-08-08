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
    public class MeasurementViewModel
    {
        #region Properties
        private IList<string> units = new List<string>();
        
        public IList<string> Units
        {
            get { return units; }
            set { units = value; }
        }
        #endregion

        #region Constructor
        public MeasurementViewModel()
        {
            PopulateComboBoxes();
        }
        #endregion

        #region Methods

        /*
         * 
         * This one
         * 
         */
        private void PopulateComboBoxes()
        {
            Units = new List<string>() { "cm", "mm", "in", "ft", "yd" };
        }
        #endregion
    }
}
