using ImageProcessing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageProcessing
{
    internal class ViewColors : Form
    {
        private Bitmap bmp;

        private IContainer components = null;

        protected internal PictureBox PictureBox;

        private Timer Timer;

        public ViewColors() {
            InitializeComponent();
            bmp = BlankBitmap(360, 360);
            DrawColorRange();
            PictureBox.Image = bmp;
            PictureBox.Update();
        }

        private Bitmap BlankBitmap(int width, int height) {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                Rectangle rect = new Rectangle(0, 0, width, height);
                graphics.FillRectangle(Brushes.White, rect);
            }
            return bitmap;
        }

        private unsafe void DrawColorRange() {
            HSV hSV = new HSV(0.0, 1.0, 1.0);
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int num = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int height = bitmapData.Height;
            int num2 = bitmapData.Width * num;
            byte* ptr = (byte*)(void*)bitmapData.Scan0;
            for (int i = 0; i < height; i++) {
                byte* ptr2 = ptr + i * bitmapData.Stride;
                for (int j = 0; j < num2; j += num) {
                    hSV.SetHue((double)(j / num));
                    Color color = hSV.ToRGB();
                    ptr2[j] = color.B;
                    ptr2[j + 1] = color.G;
                    ptr2[j + 2] = color.R;
                }
            }
            bmp.UnlockBits(bitmapData);
        }

        public void FindColor() {
            Bitmap bitmap = Screenshot();
            Bitmap image = new Bitmap(bmp);
            Bitmap bitmap2 = bitmap;
            Point position = Cursor.Position;
            int x = position.X;
            position = Cursor.Position;
            Color pixel = bitmap2.GetPixel(x, position.Y);
            PictureBox.Image = bmp;
            HSV hSV = default(HSV);
            using (Graphics graphics = Graphics.FromImage(image)) {
                hSV = pixel.ToHSV();
                graphics.DrawLine(new Pen(Color.Black), new Point((int)hSV.GetHue(), 0), new Point((int)hSV.GetHue(), 180));
                graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(300, 300, 40, 40));
                graphics.FillRectangle(new SolidBrush(hSV.ToRGB()), new Rectangle(302, 302, 36, 36));
            }
            double num = hSV.GetHue();
            image = Draw.String("Hue:\t\t" + num.ToString(), 10, 300, Color.Black, 10, image);
            num = hSV.GetSaturation();
            image = Draw.String("Saturation:\t" + num.ToString(), 10, 315, Color.Black, 10, image);
            num = hSV.GetValue();
            image = Draw.String("Value:\t\t" + num.ToString(), 10, 330, Color.Black, 10, image);
            PictureBox.Image = image;
            PictureBox.Update();
            bitmap.Dispose();
            image.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            FindColor();
        }

        public static Bitmap Screenshot() {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            int width = bounds.Width;
            bounds = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(width, bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Graphics graphics2 = graphics;
            bounds = Screen.PrimaryScreen.Bounds;
            graphics2.CopyFromScreen(0, 0, 0, 0, bounds.Size);
            graphics.Dispose();
            return bitmap;
        }

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            components = new Container();
            PictureBox = new PictureBox();
            Timer = new Timer(components);
            ((ISupportInitialize)PictureBox).BeginInit();
            base.SuspendLayout();
            PictureBox.BackColor = Color.White;
            PictureBox.Dock = DockStyle.Fill;
            PictureBox.Location = new Point(0, 0);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(360, 360);
            PictureBox.TabIndex = 0;
            PictureBox.TabStop = false;
            Timer.Enabled = true;
            Timer.Interval = 10;
            Timer.Tick += Timer_Tick;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(360, 360);
            base.Controls.Add(PictureBox);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "ViewColors";
            Text = "HSV Colors";
            ((ISupportInitialize)PictureBox).EndInit();
            base.ResumeLayout(false);
        }
    }

}
