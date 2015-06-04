using System.Collections.Generic;
using Windows.UI;

namespace Chronometer
{
    public class ColorTools
    {
        public static uint ColorToArgb(Color color)
        {

            return (uint)((color.A << 24) |
                            (color.R << 16) |
                            (color.G << 8) |
                            (color.B));
        }

        public static Color ColorFromArgb(uint color)
        {

            return Color.FromArgb(
                //(byte)((color >> 24) & 0xff),
                (byte)0xff,    // trasparent colors aren't available
                (byte)((color >> 16) & 0xff),
                (byte)((color >> 8) & 0xff),
                (byte)((color) & 0xff));
        }
    }
}
