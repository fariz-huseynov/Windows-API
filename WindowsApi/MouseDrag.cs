using System;
using System.Drawing;
using System.Threading;

namespace WindowsApi
{
    internal static class MouseDrag
    {
        public static void LeftDrag(Point point1, Point point2, int interval, int lag) {
            double num = ((double)point2.Y - (double)point1.Y) / ((double)point2.X - (double)point1.X);
            double num2 = (double)point1.X;
            double num3 = num * num2 - num * (double)point2.X + (double)point2.Y;
            if (point1 == point2) {
                throw new Exception("Points cannot be equal.");
            }
            if (interval <= 100 && interval >= 0) {
                int num4 = 0;
                Mouse.LeftDown(point1.X, point1.Y);
                if (point1.X < point2.X) {
                    for (int i = 0; i < point2.X - point1.X; i += interval) {
                        num4++;
                        num2 = (double)(point1.X + i);
                        num3 = num * num2 - num * (double)point2.X + (double)point2.Y;
                        Mouse.Move((int)num2, (int)num3);
                        Thread.Sleep(lag);
                        if (num4 > 10000) {
                            break;
                        }
                    }
                } else if (point1.X > point2.X) {
                    for (int j = 0; j < Math.Abs(point2.X - point1.X); j += interval) {
                        num4++;
                        num2 = (double)(point1.X - j);
                        num3 = num * num2 - num * (double)point2.X + (double)point2.Y;
                        Mouse.Move((int)num2, (int)num3);
                        Thread.Sleep(lag);
                        if (num4 > 10000) {
                            break;
                        }
                    }
                } else if (point1.X == point2.X && point1.Y < point2.Y) {
                    for (int k = 0; k < Math.Abs(point2.Y - point1.Y); k += interval) {
                        num4++;
                        num3 = (double)(point1.Y + k);
                        Mouse.Move((int)num2, (int)num3);
                        Thread.Sleep(lag);
                        if (num4 > 10000) {
                            break;
                        }
                    }
                } else if (point1.X == point2.X && point1.Y > point2.Y) {
                    for (int l = 0; l < Math.Abs(point2.Y - point1.Y); l += interval) {
                        num4++;
                        num3 = (double)(point1.Y - l);
                        Mouse.Move((int)num2, (int)num3);
                        Thread.Sleep(lag);
                        if (num4 > 10000) {
                            break;
                        }
                    }
                }
                Mouse.LeftUp();
                return;
            }
            throw new Exception("Interval is a percentage and therefore must be between 0 and 100.");
        }
    }

}
