using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FracDecConversion
{
    public class VolumeViewModel
    {
        #region Properties
        private IList<string> volumeUnits = new List<string>();

        public IList<string> VolumeUnits
        {
            get { return volumeUnits; }
            set { volumeUnits = value; }
        }
        #endregion

        #region Constructor
        public VolumeViewModel()
        {
            PopulateComboBoxes();
        }
        #endregion

        #region Methods
        private void PopulateComboBoxes()
        {
            VolumeUnits = new List<string>() { "in³", "ft³", "yd³", "mm³", "cm³", "cc", "m³", "mL", "L", "gal", "qt", "pt", "fl oz"};
        }
        #endregion
    }
}
