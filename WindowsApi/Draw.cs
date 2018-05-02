using System;
using System.Drawing;

namespace WindowsApi
{
    public static class Draw
    {
        public static void Circle(Color color, int x, int y, int size, int thickness, IntPtr hWnd) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            Circle(color, point.X, point.Y, size, thickness);
        }

        public static void Circle(Color color, int x, int y, int size, int thickness) {
            IntPtr dC = WinAPI.GetDC(IntPtr.Zero);
            Pen pen = new Pen(color, (float)thickness);
            using (Graphics graphics = Graphics.FromHdc(dC)) {
                graphics.DrawEllipse(pen, x - size / 2, y - size / 2, size, size);
            }
            WinAPI.ReleaseDC(IntPtr.Zero, dC);
        }

        public static void Rectangle(Color color, Rectangle rec, int thickness, IntPtr hWnd) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, rec.X, rec.Y);
            Rectangle(color, point.X, point.Y, rec.Width, rec.Height, thickness);
        }

        public static void Rectangle(Color color, int x, int y, int width, int height, int thickness, IntPtr hWnd) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            Rectangle(color, point.X, point.Y, width, height, thickness);
        }

        public static void Rectangle(Color color, Rectangle rec, int thickness) {
            Rectangle(color, rec.X, rec.Y, rec.Width, rec.Height, thickness);
        }

        public static void Rectangle(Color color, int x, int y, int width, int height, int thickness) {
            IntPtr dC = WinAPI.GetDC(IntPtr.Zero);
            Pen pen = new Pen(color, (float)thickness);
            using (Graphics graphics = Graphics.FromHdc(dC)) {
                graphics.DrawRectangle(pen, new Rectangle(x, y, width, height));
            }
            WinAPI.ReleaseDC(IntPtr.Zero, dC);
        }

        public static void String(string str, int x, int y, Color color, int fontSize, IntPtr hWnd) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            String(str, point.X, point.Y, color, fontSize);
        }

        public static void String(string str, int x, int y, Color color, int fontSize) {
            IntPtr dC = WinAPI.GetDC(IntPtr.Zero);
            Brush brush = new SolidBrush(color);
            Font font = new Font("Arial", (float)fontSize);
            using (Graphics graphics = Graphics.FromHdc(dC)) {
                graphics.DrawString(str, font, brush, new Point(x, y));
            }
            WinAPI.ReleaseDC(IntPtr.Zero, dC);
        }
    }

}
