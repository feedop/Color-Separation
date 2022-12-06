using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation.Separators
{
    public class RGBSeparator : Separator
    {
        public override string Description { get; } = "RGB";
        public override (int, int, int) Separate(int pixel, ColorProfile colorProfile)
        {
            double r = ((pixel >> 16) & 0xff) / 255.0;
            double g = ((pixel >> 8) & 0xff) / 255.0;
            double b = (pixel & 0xff) / 255.0;

            return ((int)(r * 255) << 16, (int)(g * 255) << 8, (int)(b * 255) << 0);
        }
    }
}
