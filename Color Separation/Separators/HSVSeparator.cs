using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation.Separators
{
    public class HSVSeparator : Separator
    {
        public override string Description { get; } = "HSV";

        // https://www.geeksforgeeks.org/program-change-rgb-color-model-hsv-color-model/
        public override (int, int, int) Separate(int pixel, ColorProfile colorProfile)
        {

            double r = ((pixel >> 16) & 0xff) / 255.0;
            double g = ((pixel >> 8) & 0xff) / 255.0;
            double b = (pixel & 0xff) / 255.0;

            double cmax = Math.Max(r, Math.Max(g, b)); // maximum of r, g, b
            double cmin = Math.Min(r, Math.Min(g, b)); // minimum of r, g, b
            double diff = cmax - cmin; // diff of cmax and cmin.
            double h = 0, s = 0;

            // if cmax and cmax are equal then h = 0
            if (cmax == cmin)
                h = 0;

            // if cmax equal r then compute h
            else if (cmax == r)
                h = (60 * ((g - b) / diff)) / 360.0;

            // if cmax equal g then compute h
            else if (cmax == g)
                h = (60 * ((b - r) / diff) + 120) / 360.0;

            // if cmax equal b then compute h
            else if (cmax == b)
                h = (60 * ((r - g) / diff) + 240) / 360.0;

            // if cmax equal zero
            if (cmax == 0)
                s = 0;
            else
                s = (diff / cmax);

            

            // compute v
            double v = cmax;

            return (((int)(h * 255) << 16) + ((int)(h * 255) << 8) + (int)(h * 255),
                ((int)(s * 255) << 16) + ((int)(s * 255) << 8) + (int)(s * 255),
                ((int)(v * 255) << 16) + ((int)(v * 255) << 8) + (int)(v * 255));
        }
    }
}
