using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
namespace ImageProcessing
{
    public static class Filter
    {
        public static Bitmap BlurLow(Bitmap bitmap) {
            double[,] kernel = new double[3, 3]
            {
            {
                0.0,
                1.0,
                0.0
            },
            {
                1.0,
                1.0,
                1.0
            },
            {
                0.0,
                1.0,
                0.0
            }
            };
            return Convolution(bitmap, kernel, 0.2, 0.0);
        }

        public static Bitmap BlurMedium(Bitmap bitmap) {
            double[,] kernel = new double[5, 5]
            {
            {
                0.0,
                0.0,
                1.0,
                0.0,
                0.0
            },
            {
                0.0,
                1.0,
                1.0,
                1.0,
                0.0
            },
            {
                1.0,
                1.0,
                1.0,
                1.0,
                1.0
            },
            {
                0.0,
                1.0,
                1.0,
                1.0,
                0.0
            },
            {
                0.0,
                0.0,
                1.0,
                0.0,
                0.0
            }
            };
            return Convolution(bitmap, kernel, 0.076923076923076927, 0.0);
        }

        public static Bitmap BlurHigh(Bitmap bitmap) {
            double[,] kernel = new double[7, 7]
            {
            {
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                1.0,
                1.0,
                1.0,
                0.0,
                0.0
            },
            {
                0.0,
                1.0,
                1.0,
                1.0,
                1.0,
                1.0,
                0.0
            },
            {
                1.0,
                1.0,
                1.0,
                1.0,
                1.0,
                1.0,
                1.0
            },
            {
                0.0,
                1.0,
                1.0,
                1.0,
                1.0,
                1.0,
                0.0
            },
            {
                0.0,
                0.0,
                1.0,
                1.0,
                1.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0
            }
            };
            return Convolution(bitmap, kernel, 0.04, 0.0);
        }

        public static Bitmap MotionBlur(Bitmap bitmap) {
            double[,] kernel = new double[9, 9]
            {
            {
                1.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                1.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                1.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                1.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                0.0,
                1.0
            }
            };
            return Convolution(bitmap, kernel, 0.1111111111111111, 0.0);
        }

        public static Bitmap HorizontalEdgesLow(Bitmap bitmap) {
            double[,] obj = new double[3, 3];
            obj[1, 0] = -1.0;
            obj[1, 1] = 1.0;
            double[,] kernel = obj;
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap HorizontalEdgesHigh(Bitmap bitmap) {
            double[,] kernel = new double[5, 5]
            {
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                -1.0,
                -1.0,
                2.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap VerticalEdgesLow(Bitmap bitmap) {
            double[,] obj = new double[3, 3];
            obj[0, 1] = -1.0;
            obj[1, 1] = 1.0;
            double[,] kernel = obj;
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap VerticalEdgesHigh(Bitmap bitmap) {
            double[,] kernel = new double[5, 5]
            {
            {
                0.0,
                0.0,
                -1.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                -1.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                2.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            },
            {
                0.0,
                0.0,
                0.0,
                0.0,
                0.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap EdgesLow(Bitmap bitmap) {
            double[,] kernel = new double[3, 3]
            {
            {
                -1.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                8.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                -1.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap EdgesHigh(Bitmap bitmap) {
            double[,] kernel = new double[5, 5]
            {
            {
                -1.0,
                -1.0,
                -1.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                -1.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                24.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                -1.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                -1.0,
                -1.0,
                -1.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap Sharpen(Bitmap bitmap) {
            double[,] kernel = new double[3, 3]
            {
            {
                -1.0,
                -1.0,
                -1.0
            },
            {
                -1.0,
                9.0,
                -1.0
            },
            {
                -1.0,
                -1.0,
                -1.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        public static Bitmap Emboss(Bitmap bitmap) {
            double[,] kernel = new double[3, 3]
            {
            {
                -1.0,
                -1.0,
                0.0
            },
            {
                -1.0,
                0.0,
                1.0
            },
            {
                0.0,
                1.0,
                1.0
            }
            };
            return Convolution(bitmap, kernel, 1.0, 128.0);
        }

        public static Bitmap Brighten(Bitmap bitmap, int level) {
            if (level < 0) {
                throw new ArgumentException("Level must be greater than 0.");
            }
            double[,] obj = new double[1, 1];
            obj[0, 0] = 1.0;
            double[,] kernel = obj;
            return Convolution(bitmap, kernel, 1.0, (double)level);
        }

        public static Bitmap Darken(Bitmap bitmap, int level) {
            if (level < 0) {
                throw new ArgumentException("Level must be greater than 0.");
            }
            double[,] obj = new double[1, 1];
            obj[0, 0] = 1.0;
            double[,] kernel = obj;
            return Convolution(bitmap, kernel, 1.0, (double)(level * -1));
        }

        private static Bitmap Convolution(Bitmap bitmap, double[,] kernel, double factor, double bias) {
            if (kernel.GetLength(0) != kernel.GetLength(1)) {
                throw new Exception("Kernel must be a perfect square.");
            }
            if (kernel.GetLength(0) % 2 == 0) {
                throw new Exception("Kernel width and height must be odd.");
            }
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            int num = (int)Math.Floor((double)kernel.GetLength(0) / 2.0);
            int num2 = -num;
            int num3 = num;
            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    double num4 = 0.0;
                    double num5 = 0.0;
                    double num6 = 0.0;
                    for (int k = num2; k <= num3; k++) {
                        for (int l = num2; l <= num3; l++) {
                            if (IsInBounds(bitmap, i + k, j + l) && kernel[k + num, l + num] != 0.0) {
                                double num7 = num4;
                                Color pixel = bitmap.GetPixel(i + k, j + l);
                                num4 = num7 + (double)(int)pixel.R * (kernel[k + num, l + num] * factor);
                                double num8 = num5;
                                pixel = bitmap.GetPixel(i + k, j + l);
                                num5 = num8 + (double)(int)pixel.G * (kernel[k + num, l + num] * factor);
                                double num9 = num6;
                                pixel = bitmap.GetPixel(i + k, j + l);
                                num6 = num9 + (double)(int)pixel.B * (kernel[k + num, l + num] * factor);
                            }
                        }
                    }
                    num4 += bias;
                    num5 += bias;
                    num6 += bias;
                    if (num4 < 0.0) {
                        num4 = 0.0;
                    }
                    if (num5 < 0.0) {
                        num5 = 0.0;
                    }
                    if (num6 < 0.0) {
                        num6 = 0.0;
                    }
                    if (num4 > 255.0) {
                        num4 = 255.0;
                    }
                    if (num5 > 255.0) {
                        num5 = 255.0;
                    }
                    if (num6 > 255.0) {
                        num6 = 255.0;
                    }
                    Color color = Color.FromArgb((int)num4, (int)num5, (int)num6);
                    bitmap2.SetPixel(i, j, color);
                }
            }
            return bitmap2;
        }

        public unsafe static Bitmap BlurFast(Bitmap bmp) {
            Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height);
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            BitmapData bitmapData2 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int num = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int height = bitmapData.Height;
            int num2 = bitmapData.Width * num;
            byte* ptr = (byte*)(void*)bitmapData.Scan0;
            int num3 = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int height2 = bitmapData2.Height;
            int num4 = bitmapData2.Width * num;
            byte* ptr2 = (byte*)(void*)bitmapData2.Scan0;
            for (int i = 1; i < height - 1; i++) {
                for (int j = num; j < num2 - num; j += num) {
                    byte* ptr3 = ptr + (i - 1) * bitmapData.Stride;
                    byte* ptr4 = ptr + i * bitmapData.Stride;
                    byte* ptr5 = ptr + (i + 1) * bitmapData.Stride;
                    byte* ptr6 = ptr + i * bitmapData2.Stride;
                    int num5 = ptr3[j - num];
                    int num6 = ptr3[j - num + 1];
                    int num7 = ptr3[j - num + 2];
                    int num8 = ptr3[j];
                    int num9 = ptr3[j + 1];
                    int num10 = ptr3[j + 2];
                    int num11 = ptr3[j + num];
                    int num12 = ptr3[j + num + 1];
                    int num13 = ptr3[j + num + 2];
                    int num14 = ptr4[j - num];
                    int num15 = ptr4[j - num + 1];
                    int num16 = ptr4[j - num + 2];
                    int num17 = ptr4[j];
                    int num18 = ptr4[j + 1];
                    int num19 = ptr4[j + 2];
                    int num20 = ptr4[j + num];
                    int num21 = ptr4[j + num + 1];
                    int num22 = ptr4[j + num + 2];
                    int num23 = ptr4[j - num];
                    int num24 = ptr4[j - num + 1];
                    int num25 = ptr4[j - num + 2];
                    int num26 = ptr4[j];
                    int num27 = ptr4[j + 1];
                    int num28 = ptr4[j + 2];
                    int num29 = ptr4[j + num];
                    int num30 = ptr4[j + num + 1];
                    int num31 = ptr4[j + num + 2];
                    int num32 = (num5 + num8 + num11 + num14 + num17 + num20 + num23 + num17 + num20) / 9;
                    int num33 = (num6 + num9 + num12 + num15 + num18 + num21 + num24 + num27 + num30) / 9;
                    int num34 = (num7 + num10 + num13 + num16 + num19 + num22 + num25 + num28 + num31) / 9;
                    ptr6[j] = (byte)num32;
                    ptr6[j + 1] = (byte)num33;
                    ptr6[j + 2] = (byte)num34;
                }
            }
            bmp.UnlockBits(bitmapData);
            bitmap.UnlockBits(bitmapData2);
            return bmp;
        }

        private static bool IsInBounds(Bitmap bitmap, int x, int y) {
            int width = bitmap.Width;
            int height = bitmap.Height;
            if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height) {
                return true;
            }
            return false;
        }

        private static void DebugRGB(double r, double g, double b) {
            Console.WriteLine("R: " + r);
            Console.WriteLine("G: " + g);
            Console.WriteLine("B: " + b);
            Console.WriteLine();
            Thread.Sleep(1000);
        }
    }

}
