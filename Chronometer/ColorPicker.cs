using GalaSoft.MvvmLight;
using System.ComponentModel;
using Windows.UI;

namespace Chronometer
{
    public class ColorPicker : ObservableObject
    {
        int r;

        int g;

        int b;

        public int R
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
                RaisePropertyChanged("Color");
            }
        }

        public int G
        {
            get
            {
                return g;
            }
            set
            {
                g = value;
                RaisePropertyChanged("Color");
            }
        }

        public int B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                RaisePropertyChanged("Color");
            }
        }

        public Color Color
        {
            get
            {
                return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
            }
        }
    }
}
