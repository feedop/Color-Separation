using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Color_Separation.Separators
{
    public class LabSeparator : Separator
    {
        public override string Description { get; } = "Lab";
        public override (int, int, int) Separate(int pixel, ColorProfile colorProfile)
        {
            // XYZ for white
            double Yw = 1;
            double Xw = colorProfile.Illuminant.X * Yw / colorProfile.Illuminant.Y;
            double Zw = (1 - colorProfile.Illuminant.X - colorProfile.Illuminant.Y) / colorProfile.Illuminant.Y;

            // linear equation system
            double[,] matrixArr =
            {
                { colorProfile.RedX, colorProfile.GreenX, colorProfile.BlueX },
                { colorProfile.RedY, colorProfile.GreenY, colorProfile.BlueY },
                { 1 - colorProfile.RedX - colorProfile.RedY, 1 - colorProfile.GreenX - colorProfile.GreenY, 1 - colorProfile.BlueX - colorProfile.BlueY },
            };

            var builder = Matrix<double>.Build;
            Matrix<double> matrix = builder.DenseOfArray(matrixArr);

            double[,] rightSideArr = { { Xw }, { Yw }, { Zw } };
            Matrix<double> rightSide = builder.DenseOfArray(rightSideArr);

            // solve for SR, SG, SB
            Matrix<double> S = matrix.Solve(rightSide);

            double R = ((pixel >> 16) & 0xff) / 255.0;
            double G = ((pixel >> 8) & 0xff) / 255.0;
            double B = (pixel & 0xff) / 255.0;

            R = Math.Pow(R, colorProfile.Gamma);
            G = Math.Pow(G, colorProfile.Gamma);
            B = Math.Pow(B, colorProfile.Gamma);

            double[,] RGBArr =
            {
                {R},
                {G},
                {B},
            };

            Matrix<double> RGB = builder.DenseOfArray(RGBArr);
            double[,] matrixSArr =
            {
                {matrix[0,0] * S[0, 0], matrix[0, 1] * S[1, 0], matrix[0, 2] *  S[2, 0]},
                {matrix[1,0] * S[0, 0], matrix[1, 1] * S[1, 0], matrix[1, 2] *  S[2, 0]},
                {matrix[2,0] * S[0, 0], matrix[2, 1] * S[1, 0], matrix[2, 2] *  S[2, 0]},
            };
            Matrix<double> matrixS = builder.DenseOfArray(matrixSArr);
            // calculate XYZ
            Matrix<double> XYZ = matrixS * RGB;

            // calculate L*a*b* from XYZ
            double CIE_L = ((XYZ[1, 0] / Yw > 0.008856 ? 116 * Math.Cbrt(XYZ[1, 0] / Yw) - 16 : 903.3 * XYZ[1, 0] / Yw) + 127.5) * 2;
            double CIE_a = (500 * (Math.Cbrt(XYZ[0, 0] / Xw) - Math.Cbrt(XYZ[1, 0] / Yw)) + 127.5) * 2;
            double CIE_b = (200 * (Math.Cbrt(XYZ[1, 0] / Yw) - Math.Cbrt(XYZ[2, 0] / Zw)) + 127.5) * 2;

            return (((int)(CIE_L) << 16) + ((int)(CIE_L) << 8) + (int)(CIE_L),
            ((int)(CIE_a) << 16) + ((int)(255 - CIE_a) << 8) + 127,
            ((int)(CIE_b) << 16) + (127 << 8) + ((int)(255 - CIE_b)));
        }
    }
}
