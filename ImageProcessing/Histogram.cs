using ImageProcessing;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessing
{
    public class Histogram
    {
        private int[] redBucket = new int[256];

        private int[] greenBucket = new int[256];

        private int[] blueBucket = new int[256];

        private int count = 0;

        private Bitmap bmp;

        public Histogram(Bitmap bmp) {
            Create(bmp);
            this.bmp = bmp;
        }

        private unsafe void Create(Bitmap bmp) {
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int num = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int height = bitmapData.Height;
            int num2 = bitmapData.Width * num;
            byte* ptr = (byte*)(void*)bitmapData.Scan0;
            for (int i = 0; i < height; i++) {
                byte* ptr2 = ptr + i * bitmapData.Stride;
                for (int j = 0; j < num2; j += num) {
                    int num3 = ptr2[j];
                    int num4 = ptr2[j + 1];
                    int num5 = ptr2[j + 2];
                    redBucket[num5]++;
                    greenBucket[num4]++;
                    blueBucket[num3]++;
                    count += 3;
                }
            }
            bmp.UnlockBits(bitmapData);
        }

        public int GetCount() {
            return count;
        }

        public int[] GetRed() {
            return bucketCopy(redBucket);
        }

        public int[] GetGreen() {
            return bucketCopy(greenBucket);
        }

        public int[] GetBlue() {
            return bucketCopy(blueBucket);
        }

        private int[] bucketCopy(int[] bucket) {
            int[] array = new int[256];
            Array.Copy(bucket, array, 256);
            return array;
        }

        public void ViewHistogram() {
            Thread thread = new Thread(ViewHistogramForm);
            thread.Start();
        }

        private void ViewHistogramForm() {
            double highestValue = (double)GetHighestValue();
            ViewHistogram viewHistogram = new ViewHistogram(GetRed(), highestValue, Color.Red);
            viewHistogram.Show();
            ViewHistogram viewHistogram2 = new ViewHistogram(GetGreen(), highestValue, Color.Green);
            viewHistogram2.Show();
            ViewHistogram viewHistogram3 = new ViewHistogram(GetBlue(), highestValue, Color.Blue);
            viewHistogram3.Show();
            Application.Run();
        }

        private int GetHighestValue() {
            int num = 0;
            for (int i = 0; i < 256; i++) {
                if (redBucket[i] > num) {
                    num = redBucket[i];
                }
                if (greenBucket[i] > num) {
                    num = greenBucket[i];
                }
                if (blueBucket[i] > num) {
                    num = blueBucket[i];
                }
            }
            return num;
        }

        public double CompareTo(Histogram hist) {
            int num = 0;
            for (int i = 0; i < 256; i++) {
                num += Math.Min(redBucket[i], hist.GetRed()[i]);
                num += Math.Min(greenBucket[i], hist.GetGreen()[i]);
                num += Math.Min(blueBucket[i], hist.GetBlue()[i]);
            }
            return (double)num / (double)count;
        }
    }

}
