
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.Graphics
{
    public static class ColorUtils
    {
        /// <summary>
        /// http://csharphelper.com/blog/2014/09/map-numeric-values-to-colors-in-a-rainbow-in-c/
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        // Map a value to a rainbow color.
        public static Color MapRainbowColor(
            double value, double red_value, double blue_value)
        {
            // Convert into a value between 0 and 1023.
            int int_value = (int)(1023 * (value - red_value) /
                (blue_value - red_value));

            // Map different color bands.
            if (int_value < 256)
            {
                // Red to yellow. (255, 0, 0) to (255, 255, 0).
                return Color.FromArgb(255, int_value, 0);
            }
            else if (int_value < 512)
            {
                // Yellow to green. (255, 255, 0) to (0, 255, 0).
                int_value -= 256;
                return Color.FromArgb(255 - int_value, 255, 0);
            }
            else if (int_value < 768)
            {
                // Green to aqua. (0, 255, 0) to (0, 255, 255).
                int_value -= 512;
                return Color.FromArgb(0, 255, int_value);
            }
            else
            {
                // Aqua to blue. (0, 255, 255) to (0, 0, 255).
                int_value -= 768;
                return Color.FromArgb(0, 255 - int_value, 255);
            }
        }
    }
}
