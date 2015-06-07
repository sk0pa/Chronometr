using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Chronometer
{
    public class ActivityForBinding
    {
        public string Name{ get; set; }

        public Color Color { get; set; }

        public double TotalSeconds { get; set; }

        public string TotalTime 
        {
            get
            {
                return TimeSpan.FromSeconds(TotalSeconds).ToString(@"dd\.hh\:mm\:ss");
            }
        }

        public double X2 { get; set; }
    }
}
