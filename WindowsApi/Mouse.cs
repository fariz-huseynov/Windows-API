using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsApi
{
    public static class Mouse
    {
        public static void LeftDrag(IntPtr hWnd, Point point1, Point point2, int interval, int lag) {
            point1 = Conversion.ConvertToWindowCoordinates(hWnd, point1.X, point1.Y);
            point2 = Conversion.ConvertToWindowCoordinates(hWnd, point2.X, point2.Y);
            LeftDrag(point1, point2, interval, lag);
        }

        public static void LeftDrag(Point point1, Point point2, int interval, int lag) {
            MouseDrag.LeftDrag(point1, point2, interval, lag);
        }

        public static void LeftClick() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            LeftClick(x, position.Y);
        }

        public static void LeftClick(int x, int y) {
            LeftClick(x, y, new Random().Next(20, 30));
        }

        public static void LeftClick(IntPtr hWnd, int x, int y) {
            LeftClick(hWnd, x, y, new Random().Next(20, 30));
        }

        public static void LeftClick(IntPtr hWnd, int x, int y, int delay) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            LeftClick(point.X, point.Y, delay);
        }

        public static void LeftClick(int x, int y, int delay) {
            Move(x, y);
            LeftDown();
            Thread.Sleep(delay);
            LeftUp();
        }

        public static void RightClick() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            RightClick(x, position.Y);
        }

        public static void RightClick(int x, int y) {
            RightClick(x, y, new Random().Next(20, 30));
        }

        public static void RightClick(IntPtr hWnd, int x, int y) {
            RightClick(hWnd, x, y, new Random().Next(20, 30));
        }

        public static void RightClick(IntPtr hWnd, int x, int y, int delay) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            RightClick(point.X, point.Y, delay);
        }

        public static void RightClick(int x, int y, int delay) {
            Move(x, y);
            RightDown();
            Thread.Sleep(delay);
            RightUp();
        }

        public static void MiddleClick() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            MiddleClick(x, position.Y);
        }

        public static void MiddleClick(int x, int y) {
            MiddleClick(x, y, new Random().Next(20, 30));
        }

        public static void MiddleClick(IntPtr hWnd, int x, int y) {
            MiddleClick(hWnd, x, y, new Random().Next(20, 30));
        }

        public static void MiddleClick(IntPtr hWnd, int x, int y, int delay) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            MiddleClick(point.X, point.Y, delay);
        }

        public static void MiddleClick(int x, int y, int delay) {
            Move(x, y);
            MiddleDown();
            Thread.Sleep(delay);
            MiddleUp();
        }

        public static void LeftDown() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            LeftDown(x, position.Y);
        }

        public static void LeftDown(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            LeftDown(point.X, point.Y);
        }

        public static void LeftDown(int x, int y) {
            Move(x, y);
            MouseFunction(2u);
        }

        public static void LeftUp() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            LeftUp(x, position.Y);
        }

        public static void LeftUp(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            LeftUp(point.X, point.Y);
        }

        public static void LeftUp(int x, int y) {
            Move(x, y);
            MouseFunction(4u);
        }

        public static void RightDown() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            RightDown(x, position.Y);
        }

        public static void RightDown(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            RightDown(point.X, point.Y);
        }

        public static void RightDown(int x, int y) {
            Move(x, y);
            MouseFunction(8u);
        }

        public static void RightUp() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            RightUp(x, position.Y);
        }

        public static void RightUp(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            RightUp(point.X, point.Y);
        }

        public static void RightUp(int x, int y) {
            Move(x, y);
            MouseFunction(16u);
        }

        public static void MiddleDown() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            MiddleDown(x, position.Y);
        }

        public static void MiddleDown(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            MiddleDown(point.X, point.Y);
        }

        public static void MiddleDown(int x, int y) {
            Move(x, y);
            MouseFunction(32u);
        }

        public static void MiddleUp() {
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            MiddleUp(x, position.Y);
        }

        public static void MiddleUp(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            MiddleUp(point.X, point.Y);
        }

        public static void MiddleUp(int x, int y) {
            Move(x, y);
            MouseFunction(64u);
        }

        public static void Move(IntPtr hWnd, int x, int y) {
            Point point = Conversion.ConvertToWindowCoordinates(hWnd, x, y);
            Move(point.X, point.Y);
        }

        public static void Move(int x, int y) {
            Cursor.Position = new Point(x, y);
        }

        private static void MouseFunction(uint flag) {
            Structs.INPUT ıNPUT = default(Structs.INPUT);
            ıNPUT.Type = 0u;
            ıNPUT.Data.Mouse.Flags = flag;
            WinAPI.SendInput(1u, ref ıNPUT, Marshal.SizeOf(default(Structs.INPUT)));
        }
    }

}
