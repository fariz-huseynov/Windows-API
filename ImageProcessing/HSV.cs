using ImageProcessing;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessing
{
    public class HSV
    {
        private double H;

        private double S;

        private double V;

        public HSV(double hue, double saturation, double value) {
            SetHue(hue);
            SetSaturation(saturation);
            SetValue(value);
        }

        public void SetHue(double hue) {
            if (hue < 0.0) {
                double num = hue * -1.0;
                double num2 = Math.Round(num / 360.0);
                H = hue + 360.0 * (num2 + 1.0);
            } else {
                H = hue % 360.0;
            }
        }

        public void SetSaturation(double saturation) {
            if (saturation < 0.0) {
                S = 0.0;
            } else if (saturation > 1.0) {
                S = 1.0;
            } else {
                S = saturation;
            }
        }

        public void SetValue(double value) {
            if (value < 0.0) {
                V = 0.0;
            } else if (value > 1.0) {
                V = 1.0;
            } else {
                V = value;
            }
        }

        public double GetHue() {
            return H;
        }

        public double GetSaturation() {
            return S;
        }

        public double GetValue() {
            return V;
        }

        public Color ToRGB() {
            if (H < 0.0 || H > 360.0) {
                throw new Exception("Hue must be between 0 and 360.");
            }
            if (S < 0.0 || S > 1.0) {
                throw new Exception("Saturation must be between 0 and 1.");
            }
            if (V < 0.0 || V > 1.0) {
                throw new Exception("Value must be between 0 and 1.");
            }
            double num = V * S;
            double num2 = num * (1.0 - Math.Abs(H / 60.0 % 2.0 - 1.0));
            double num3 = V - num;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            switch ((int)Math.Floor(H / 60.0)) {
                case 0:
                    num4 = num;
                    num5 = num2;
                    num6 = 0.0;
                    break;
                case 1:
                    num4 = num2;
                    num5 = num;
                    num6 = 0.0;
                    break;
                case 2:
                    num4 = 0.0;
                    num5 = num;
                    num6 = num2;
                    break;
                case 3:
                    num4 = 0.0;
                    num5 = num2;
                    num6 = num;
                    break;
                case 4:
                    num4 = num2;
                    num5 = 0.0;
                    num6 = num;
                    break;
                case 5:
                    num4 = num;
                    num5 = 0.0;
                    num6 = num2;
                    break;
            }
            int red = (int)Math.Ceiling((num4 + num3) * 255.0);
            int green = (int)Math.Ceiling((num5 + num3) * 255.0);
            int blue = (int)Math.Ceiling((num6 + num3) * 255.0);
            return Color.FromArgb(red, green, blue);
        }

        public HSV Clone() {
            return new HSV(H, S, V);
        }

        public static void ViewColors() {
            Thread thread = new Thread(ViewColorsForm);
            thread.Start();
        }

        private static void ViewColorsForm() {
            ViewColors viewColors = new ViewColors();
            viewColors.Show();
            Application.Run();
        }

        public int CompareHue(HSV color) {
            int num = (int)Math.Abs(H - color.GetHue());
            return (num >= 180) ? (360 - num) : num;
        }
    }

}
