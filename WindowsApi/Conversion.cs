using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsApi
{
    internal class Conversion
    {
        public static Point ConvertToWindowCoordinates(IntPtr hWnd, int x, int y) {
            Structs.Rect rect = default(Structs.Rect);
            WinAPI.GetWindowRect(hWnd, out rect);
            return new Point(rect.X + x, rect.Y + y);
        }

        public static Point GetCoordinateRelativeToWindow(IntPtr hWnd) {
            Structs.Rect rect = default(Structs.Rect);
            WinAPI.GetWindowRect(hWnd, out rect);
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            int y = position.Y;
            return new Point(x - rect.X, y - rect.Y);
        }
    }

}
