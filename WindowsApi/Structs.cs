using System;
using System.Drawing;
using System.Runtime.InteropServices; 

namespace WindowsApi
{
    internal static class Structs
    {
        protected internal struct INPUT
        {
            public uint Type;

            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public int X;

            public int Y;

            public uint MouseData;

            public uint Flags;

            public uint Time;

            public IntPtr ExtraInfo;
        }

        internal struct Rect
        {
            private int _Left;

            private int _Top;

            private int _Right;

            private int _Bottom;

            public int X {
                get {
                    return _Left;
                }
                set {
                    _Left = value;
                }
            }

            public int Y {
                get {
                    return _Top;
                }
                set {
                    _Top = value;
                }
            }

            public int Left {
                get {
                    return _Left;
                }
                set {
                    _Left = value;
                }
            }

            public int Top {
                get {
                    return _Top;
                }
                set {
                    _Top = value;
                }
            }

            public int Right {
                get {
                    return _Right;
                }
                set {
                    _Right = value;
                }
            }

            public int Bottom {
                get {
                    return _Bottom;
                }
                set {
                    _Bottom = value;
                }
            }

            public int Height {
                get {
                    return _Bottom - _Top;
                }
                set {
                    _Bottom = value + _Top;
                }
            }

            public int Width {
                get {
                    return _Right - _Left;
                }
                set {
                    _Right = value + _Left;
                }
            }

            public Point Location {
                get {
                    return new Point(Left, Top);
                }
                set {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }

            public Size Size {
                get {
                    return new Size(Width, Height);
                }
                set {
                    _Right = value.Width + _Left;
                    _Bottom = value.Height + _Top;
                }
            }

            public Rect(Rect Rectangle) {
                this = new Rect(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }

            public Rect(int Left, int Top, int Right, int Bottom) {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }

            public static implicit operator Rectangle(Rect Rectangle) {
                return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
            }

            public static implicit operator Rect(Rectangle Rectangle) {
                return new Rect(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }

            public static bool operator ==(Rect Rectangle1, Rect Rectangle2) {
                return Rectangle1.Equals(Rectangle2);
            }

            public static bool operator !=(Rect Rectangle1, Rect Rectangle2) {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString() {
                return "{Left: " + _Left + "; Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }

            public override int GetHashCode() {
                return ToString().GetHashCode();
            }

            public bool Equals(Rect Rectangle) {
                if (Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right) {
                    return Rectangle.Bottom == _Bottom;
                }
                return false;
            }

            public override bool Equals(object Object) {
                if (Object is Rect) {
                    return Equals((Rect)Object);
                }
                if (Object is System.Drawing.Rectangle) {
                    return Equals(new Rect((Rectangle)Object));
                }
                return false;
            }
        }
    }

}