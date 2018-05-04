using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace WindowsApi
{
    public static class Window
    {
        public static bool DoesExist(string windowTitle) {
            return WinAPI.FindWindow(null, windowTitle) != IntPtr.Zero;
        }

        public static IntPtr Get(string windowTitle) {
            IntPtr ıntPtr = WinAPI.FindWindow(null, windowTitle);
            if (ıntPtr == IntPtr.Zero) {
                throw new Exception("Window not found.");
            }
            return ıntPtr;
        }

        public static IntPtr GetControlOnWindow(IntPtr windowInt, string controlName, string controlTitle) {
            IntPtr ıntPtr = WinAPI.FindWindowEx(windowInt, IntPtr.Zero, controlName, controlTitle);

            if (ıntPtr == IntPtr.Zero) {
                throw new Exception("Control not found.");
            }
            return ıntPtr;
        }

        public static IntPtr GetFocused() {
            return WinAPI.GetForegroundWindow();
        }

        public static void SetFocused(IntPtr hWnd) {
            WinAPI.SetForegroundWindow(hWnd);
        }

        public static bool IsFocused(IntPtr hWnd) {
            IntPtr foregroundWindow = WinAPI.GetForegroundWindow();
            if (!(hWnd == IntPtr.Zero) && !(foregroundWindow == IntPtr.Zero)) {
                return hWnd == foregroundWindow;
            }
            return false;
        }

        public static void Move(IntPtr hWnd, int x, int y) {
            WinAPI.SetWindowPos(hWnd, 0, x, y, 0, 0, 1);
        }

        public static void Resize(IntPtr hWnd, int width, int height) {
            WinAPI.SetWindowPos(hWnd, 0, 0, 0, width, height, 2);
        }

        public static void Hide(IntPtr hWnd) {
            WinAPI.SetWindowPos(hWnd, 0, 0, 0, 0, 0, 128);
        }

        public static void Show(IntPtr hWnd) {
            WinAPI.SetWindowPos(hWnd, 0, 0, 0, 0, 0, 64);
        }

        public static Rectangle GetDimensions(IntPtr hWnd) {
            Structs.Rect rect = default(Structs.Rect);
            WinAPI.GetWindowRect(hWnd, out rect);
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Size GetSize(IntPtr hWnd) {
            Rectangle dimensions = GetDimensions(hWnd);
            return new Size(dimensions.Width, dimensions.Height);
        }

        public static Point GetLocation(IntPtr hWnd) {
            Rectangle dimensions = GetDimensions(hWnd);
            return new Point(dimensions.X, dimensions.Y);
        }

        public static string GetTitle(IntPtr hWnd) {
            StringBuilder stringBuilder = new StringBuilder(256);
            if (WinAPI.GetWindowText(hWnd, stringBuilder, 256) > 0) {
                return stringBuilder.ToString();
            }
            return null;
        }

        public static void SetTitle(IntPtr hWnd, string title) {
            WinAPI.SetWindowText(hWnd, title);
        }

        public static void Maximize(IntPtr hWnd) {
            WinAPI.ShowWindow(hWnd, 3);
        }

        public static void Minimize(IntPtr hWnd) {
            WinAPI.ShowWindow(hWnd, 6);
        }

        public static void Normalize(IntPtr hWnd) {
            WinAPI.ShowWindow(hWnd, 1);
        }

        public static Bitmap Screenshot(IntPtr hWnd) {
            Normalize(hWnd);
            Structs.Rect rect;
            WinAPI.GetWindowRect(hWnd, out rect);
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            IntPtr hdc = graphics.GetHdc();
            WinAPI.PrintWindow(hWnd, hdc, 0);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
            return bitmap;
        }

        public static void RemoveMenu(IntPtr hWnd) {
            IntPtr menu = WinAPI.GetMenu(hWnd);
            int menuItemCount = WinAPI.GetMenuItemCount(menu);
            for (int i = 0; i < menuItemCount; i++) {
                WinAPI.RemoveMenu(menu, 0u, 5120u);
            }
            WinAPI.DrawMenuBar(hWnd);
        }

        public static void Close(IntPtr hWnd) {
            WinAPI.EndTask(hWnd, true, true);
        }

        public static void DisableCloseButton(IntPtr hWnd) {
            IntPtr systemMenu = WinAPI.GetSystemMenu(hWnd, false);
            if (systemMenu != IntPtr.Zero) {
                int menuItemCount = WinAPI.GetMenuItemCount(systemMenu);
                if (menuItemCount > 0) {
                    WinAPI.RemoveMenu(systemMenu, (uint)(menuItemCount - 1), 5120u);
                    WinAPI.RemoveMenu(systemMenu, (uint)(menuItemCount - 2), 5120u);
                    WinAPI.DrawMenuBar(hWnd);
                }
            }
        }

        public static void DisableMaximizeButton(IntPtr hWnd) {
            int windowLong = WinAPI.GetWindowLong(hWnd, -16);
            WinAPI.SetWindowLong(hWnd, -16, windowLong & -65537);
        }

        public static void DisableMinimizeButton(IntPtr hWnd) {
            int windowLong = WinAPI.GetWindowLong(hWnd, -16);
            WinAPI.SetWindowLong(hWnd, -16, windowLong & -131073);
        }

        public static void FlipLeft(IntPtr hWnd) {
            int windowLong = WinAPI.GetWindowLong(hWnd, -20);
            WinAPI.SetWindowLong(hWnd, -20, windowLong | 0x400000);
        }

        public static void FlipRight(IntPtr hWnd) {
            WinAPI.SetWindowLong(hWnd, -20, 0);
        }

        public static void EnableMouseTransparency(IntPtr hWnd) {
            WinAPI.SetWindowLong(hWnd, -20, Convert.ToInt32((long)(WinAPI.GetWindowLong(hWnd, -20) | 0x80000) | 32L));
        }

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



        public static void LockWorkStation() {
            WinAPI.LockWorkStation();
        }

        public static void LogOff() {
            WinAPI.ExitWindowsEx(0, 0);
        }

        public static void Hibernate() {
            WinAPI.SetSuspendState(true, true, true);
        }
        public static void Sleep() {
            WinAPI.SetSuspendState(false, true, true);
        }

        public static void SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam) {
            WinAPI.SendMessage(hWnd, wMsg, wParam, lParam);
        }

    }

}
