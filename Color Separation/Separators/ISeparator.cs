using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation.Separators
{
    internal interface ISeparator
    {
        (byte, byte, byte) Separate(byte r, byte g, byte b);
    }
}
