using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation
{
    public class Illuminant
    {
        public string Description { get; init; }
        public double X { get; set; }
        public double Y { get; set; }

        public Illuminant(string name, double x, double y)
        {
            Description = name;
            X = x;
            Y = y;
        }

        public void CustomFromIlluminant(Illuminant illuminant)
        {
            X = illuminant.X;
            Y = illuminant.Y;
        }

        public static List<Illuminant> InitializeIlluminants()
        {
            var illuminants = new List<Illuminant>();
            illuminants.Add(new Illuminant("A (2865 K)", 0.44757, 0.40744));
            illuminants.Add(new Illuminant("B (4874 K)", 0.34840, 0.35160));
            illuminants.Add(new Illuminant("C (6774 K)", 0.31006, 0.31615));
            illuminants.Add(new Illuminant("D50 (5000 K)", 0.34567, 0.35850));
            illuminants.Add(new Illuminant("D55 (5500 K)", 0.33242, 0.34743));
            illuminants.Add(new Illuminant("D65 (6504 K)", 0.31273, 0.32902));
            illuminants.Add(new Illuminant("D75 (7500 K)", 0.29902, 0.31485));
            illuminants.Add(new Illuminant("9300 K", 0.28480, 0.29320));
            illuminants.Add(new Illuminant("E (5400 K)", 0.33333, 0.33333));
            illuminants.Add(new Illuminant("F2 (4200 K)", 0.37207, 0.37512));
            illuminants.Add(new Illuminant("F7 (6500 K)", 0.31285, 0.32918));
            illuminants.Add(new Illuminant("F11 (4000 K)", 0.38054, 0.37691));
            illuminants.Add(new Illuminant("Custom Illuminant", 0.31273, 0.32902));
            return illuminants;
        }

    }
}