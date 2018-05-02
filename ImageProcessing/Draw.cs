using System.Drawing;
namespace ImageProcessing
{
    public static class Draw
    {
        public static Bitmap Circle(Color color, int x, int y, int size, int thickness, Bitmap bmp) {
            Pen pen = new Pen(color, (float)thickness);
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
            }
            return bmp;
        }

        public static Bitmap Rectangle(Color color, Rectangle rec, int thickness, Bitmap bmp) {
            return Rectangle(color, rec.X, rec.Y, rec.Width, rec.Height, thickness, bmp);
        }

        public static Bitmap Rectangle(Color color, int x, int y, int width, int height, int thickness, Bitmap bmp) {
            Pen pen = new Pen(color, (float)thickness);
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawRectangle(pen, new Rectangle(x, y, width, height));
            }
            return bmp;
        }

        public static Bitmap String(string str, int x, int y, Color color, int fontSize, Bitmap bmp) {
            Brush brush = new SolidBrush(color);
            Font font = new Font("Arial", (float)fontSize);
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.DrawString(str, font, brush, new Point(x, y));
            }
            return bmp;
        }
    }
}
