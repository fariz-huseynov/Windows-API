using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class ColorExtension
    {
        public static HSV ToHSV(this Color color) {
            double r = (double)(int)color.R / 255.0;
            double g = (double)(int)color.G / 255.0;
            double b = (double)(int)color.B / 255.0;
            double max = GetMax(color);
            double min = GetMin(color);
            double avg = max - min;
            double hue = CalculateHue(r, g, b, max, avg);
            double saturation = CalculateSaturation(max, avg);
            double value = CalculateValue(max);
            return new HSV(hue, saturation, value);
        }

        private static double GetMax(Color color) {
            double num = (double)(int)color.R / 255.0;
            double num2 = (double)(int)color.G / 255.0;
            double num3 = (double)(int)color.B / 255.0;
            double num4 = 0.0;
            if (num > num4) {
                num4 = num;
            }
            if (num2 > num4) {
                num4 = num2;
            }
            if (num3 > num4) {
                num4 = num3;
            }
            return num4;
        }

        private static double GetMin(Color color) {
            double num = (double)(int)color.R / 255.0;
            double num2 = (double)(int)color.G / 255.0;
            double num3 = (double)(int)color.B / 255.0;
            double num4 = double.PositiveInfinity;
            if (num < num4) {
                num4 = num;
            }
            if (num2 < num4) {
                num4 = num2;
            }
            if (num3 < num4) {
                num4 = num3;
            }
            return num4;
        }

        private static double CalculateHue(double R, double G, double B, double cMax, double avg) {
            if (avg == 0.0) {
                return 0.0;
            }
            if (cMax == R) {
                return 60.0 * ((G - B) / avg % 6.0);
            }
            if (cMax == G) {
                return 60.0 * ((B - R) / avg + 2.0);
            }
            if (cMax == B) {
                return 60.0 * ((R - G) / avg + 4.0);
            }
            return double.NaN;
        }

        private static double CalculateSaturation(double cMax, double avg) {
            if (cMax == 0.0) {
                return 0.0;
            }
            if (cMax != 0.0) {
                return avg / cMax;
            }
            return double.NaN;
        }

        private static double CalculateValue(double cMax) {
            return cMax;
        }
    }
}
