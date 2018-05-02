using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public static class Tools
    {
        public static Bitmap Crop(Bitmap bmp, Rectangle rec) {
            if (rec.Width > bmp.Width || rec.Height > bmp.Height) {
                throw new Exception("Region cannot be larger then the image.");
            }
            bmp = bmp.Clone(rec, bmp.PixelFormat);
            return bmp;
        }

        public static Bitmap Resize(Image image, int width, int height) {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (ImageAttributes ımageAttributes = new ImageAttributes()) {
                    ımageAttributes.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ımageAttributes);
                }
            }
            return bitmap;
        }

        public static void Copy(Bitmap target, Bitmap source, int x, int y) {
            Graphics graphics = Graphics.FromImage(target);
            graphics.DrawImage(source, x, y);
        }

        public static Bitmap BlankBitmap(int width, int height) {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                Rectangle rect = new Rectangle(0, 0, width, height);
                graphics.FillRectangle(Brushes.White, rect);
            }
            return bitmap;
        }
    }
}
