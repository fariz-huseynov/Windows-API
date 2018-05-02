using System.Drawing;


namespace ImageProcessing
{
    public static class Channels
    {
        public static Bitmap GreenToRed(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.R, color.G);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap BlueToRed(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.G, color.R);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap RedToGreen(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.G, color.G, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap BlueToGreen(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.G, color.G);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap RedToBlue(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.B, color.R, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap GreenToBlue(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.B, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap RemoveRed(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap2.GetPixel(i, j);
                    color = Color.FromArgb(0, color.G, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap RemoveGreen(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap2.GetPixel(i, j);
                    color = Color.FromArgb(color.R, 0, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap RemoveBlue(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap2.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.G, 0);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap SwapRedAndGreen(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.G, color.R, color.B);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap SwapRedAndBlue(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.B, color.G, color.R);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap SwapGreenAndBlue(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.R, color.B, color.G);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }
    }

}
