using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_Separation
{
    public class ColorProfile
    {
        public string Description { get; init; }
        public Illuminant Illuminant { get; set; }
        public double RedX { get; set; }
        public double RedY { get; set; }
        public double GreenX { get; set; }
        public double GreenY { get; set; }
        public double BlueX { get; set; }
        public double BlueY { get; set; }
        public double Gamma { get; set; }

        public ColorProfile(string description, Illuminant iluminant, double redX, double redY, double greenX, double greenY, double blueX, double blueY, double gamma)
        {
            Description = description;
            Illuminant = iluminant;
            RedX = redX;
            RedY = redY;
            GreenX = greenX;
            GreenY = greenY;
            BlueX = blueX;
            BlueY = blueY;
            Gamma = gamma;
        }

        public void CustomFromProfile(ColorProfile colorProfile)
        {
            Illuminant = colorProfile.Illuminant;
            RedX = colorProfile.RedX;
            RedY = colorProfile.RedY;
            GreenX = colorProfile.GreenX;
            GreenY = colorProfile.GreenY;
            BlueX = colorProfile.BlueX;
            BlueY = colorProfile.BlueY;
            Gamma = colorProfile.Gamma;
        }

        public static List<ColorProfile> InitializeColorProfiles(List<Illuminant> illuminants)
        {
            var colorProfiles = new List<ColorProfile>();
            colorProfiles.Add(new ColorProfile("sRGB", illuminants[5], 0.6400, 0.3300, 0.3000, 0.6000, 0.1500, 0.0600, 2.2));
            colorProfiles.Add(new ColorProfile("Adobe RGB", illuminants[5], 0.6400, 0.3300, 0.2100, 0.7100, 0.1500, 0.0600, 2.2));
            colorProfiles.Add(new ColorProfile("Apple RGB", illuminants[5], 0.6250, 0.3400, 0.2800, 0.5950, 0.1550, 0.0700, 1.8));
            colorProfiles.Add(new ColorProfile("CIE RGB", illuminants[8], 0.7350, 0.2650, 0.2740, 0.7170, 0.16700, 0.0700, 2.2));
            colorProfiles.Add(new ColorProfile("Wide Gamut", illuminants[3], 0.7347, 0.2653, 0.1152, 0.8264, 0.1566, 0.0177, 1.2));
            colorProfiles.Add(new ColorProfile("Custom color profile", illuminants[5], 0.6400, 0.3300, 0.3000, 0.6000, 0.1500, 0.0600, 2.2));

            return colorProfiles;
        }
    }
}
