using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation.Separators
{
    public abstract class Separator
    {
        public abstract string Description { get; }
        public abstract (int, int, int) Separate(int pixel, ColorProfile colorProfile);
    }
}
