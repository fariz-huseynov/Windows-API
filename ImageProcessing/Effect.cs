using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public static class Effect
    {
        public static Bitmap Threshold(Bitmap bmp) {
            return Threshold(bmp, 200);
        }

        public static Bitmap Threshold(Bitmap bmp, int T) {
            return Threshold(bmp, new int[1]
            {
            T
            });
        }

        public unsafe static Bitmap Threshold(Bitmap bmp, int[] T) {
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int height = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;
            byte* PtrFirstPixel = (byte*)(void*)bitmapData.Scan0;
            Parallel.For(0, height, delegate (int y)
            {
                byte* ptr = PtrFirstPixel + y * bitmapData.Stride;
                for (int i = 0; i < widthInBytes; i += bytesPerPixel) {
                    int num = ptr[i];
                    int num2 = ptr[i + 1];
                    int num3 = ptr[i + 2];
                    int num4 = (num + num3 + num2) / 3;
                    Color color = Color.White;
                    int num5 = 255;
                    for (int num6 = T.Length - 1; num6 >= 0; num6--) {
                        num5 -= 255 / T.Length;
                        if (num4 <= T[num6]) {
                            color = Color.FromArgb(num5, num5, num5);
                        }
                    }
                    num = color.B;
                    num2 = color.G;
                    num3 = color.R;
                    ptr[i] = (byte)num;
                    ptr[i + 1] = (byte)num2;
                    ptr[i + 2] = (byte)num3;
                }
            });
            bmp.UnlockBits(bitmapData);
            return bmp;
        }

        public unsafe static Bitmap Invert(Bitmap bmp) {
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int height = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;
            byte* PtrFirstPixel = (byte*)(void*)bitmapData.Scan0;
            Parallel.For(0, height, delegate (int y)
            {
                byte* ptr = PtrFirstPixel + y * bitmapData.Stride;
                for (int i = 0; i < widthInBytes; i += bytesPerPixel) {
                    int num = ptr[i];
                    int num2 = ptr[i + 1];
                    int num3 = ptr[i + 2];
                    num = 255 - num;
                    num2 = 255 - num2;
                    num3 = 255 - num3;
                    ptr[i] = (byte)num;
                    ptr[i + 1] = (byte)num2;
                    ptr[i + 2] = (byte)num3;
                }
            });
            bmp.UnlockBits(bitmapData);
            return bmp;
        }


        public static Bitmap Cartoonify(Bitmap bitmap) {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    Color color = bitmap.GetPixel(i, j);
                    double num = 64.0;
                    int red = (int)(Math.Floor((double)(int)color.R / num) * num);
                    int green = (int)(Math.Floor((double)(int)color.G / num) * num);
                    int blue = (int)(Math.Floor((double)(int)color.B / num) * num);
                    color = Color.FromArgb(red, green, blue);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }
    }
}
