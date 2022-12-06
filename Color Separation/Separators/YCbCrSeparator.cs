using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation.Separators
{
    public class YCbCrSeparator : Separator
    {
        public override string Description { get; } = "YCbCr";
        public override (int, int, int) Separate(int pixel, ColorProfile colorProfile)
        {
            double R = ((pixel >> 16) & 0xff) / 255.0;
            double G = ((pixel >> 8) & 0xff) / 255.0;
            double B = (pixel & 0xff) / 255.0;

            double Y = 0.299 * R + 0.587 * G + 0.114 * B;
            double Cb = (B - Y) / 1.772 + 0.5;
            double Cr = (R - Y) / 1.402 + 0.5;

            return (((int)(Y * 255) << 16) + ((int)(Y * 255) << 8) + (int)(Y * 255),
            (127 << 16) + ((int)((1 - Cb) * 255) << 8) + (int)(Cb * 255),
            ((int)(Cr * 255) << 16) + ((int)((1 - Cr) * 255) << 8) + 127);
        }
    }
}
