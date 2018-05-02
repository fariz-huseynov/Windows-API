using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsApi
{
    public static class Desktop
    {
        public static Bitmap Screenshot() {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            int width = bounds.Width;
            bounds = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(width, bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            bounds = Screen.PrimaryScreen.Bounds;
            graphics.CopyFromScreen(0, 0, 0, 0, bounds.Size);
            graphics.Dispose();
            return bitmap;
        }

        public static void HideTaskBar() {
            IntPtr desktopWindow = WinAPI.GetDesktopWindow();
            IntPtr hWnd = WinAPI.FindWindowEx(desktopWindow, 0, "button", 0);
            IntPtr hWnd2 = WinAPI.FindWindowEx(desktopWindow, 0, "Shell_TrayWnd", 0);
            WinAPI.SetWindowPos(hWnd, 0, 0, 0, 0, 0, 128);
            WinAPI.SetWindowPos(hWnd2, 0, 0, 0, 0, 0, 128);
        }

        public static void ShowTaskBar() {
            IntPtr desktopWindow = WinAPI.GetDesktopWindow();
            IntPtr hWnd = WinAPI.FindWindowEx(desktopWindow, 0, "button", 0);
            IntPtr hWnd2 = WinAPI.FindWindowEx(desktopWindow, 0, "Shell_TrayWnd", 0);
            WinAPI.SetWindowPos(hWnd, 0, 0, 0, 0, 0, 64);
            WinAPI.SetWindowPos(hWnd2, 0, 0, 0, 0, 0, 64);
        }

        public static int GetWidth() {
            return Screen.PrimaryScreen.Bounds.Width;
        }

        public static int GetHeight() {
            return Screen.PrimaryScreen.Bounds.Height;
        }
    }

}
