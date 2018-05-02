using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ImageProcessing
{
    internal class ViewHistogram : Form
    {
        private int[] colorBucket;

        private Bitmap canvas;

        private int width;

        private int height;

        private Point center;

        private double highestValue;

        private Color color = Color.Black;

        private IContainer components = null;

        protected internal PictureBox PictureBox;

        public ViewHistogram(int[] colorBucket, double highestValue, Color color) {
            InitializeComponent();
            this.colorBucket = colorBucket;
            this.highestValue = highestValue;
            this.color = color;
            width = PictureBox.Width;
            height = PictureBox.Height;
            canvas = BlankBitmap(width, height);
            SetTitle();
            DrawHistogram();
        }

        private void SetTitle() {
            if (color == Color.Red) {
                Text = "Histogram - Red";
            } else if (color == Color.Green) {
                Text = "Histogram - Green";
            } else if (color == Color.Blue) {
                Text = "Histogram - Blue";
            }
        }

        private void DrawHistogram() {
            DrawAxis();
            DrawValues();
            PictureBox.Image = canvas;
            PictureBox.Update();
        }

        private void DrawAxis() {
            using (Graphics graphics = Graphics.FromImage(canvas)) {
                Pen pen = new Pen(Color.Black);
                int num = 5;
                center = new Point(num, 256 + num);
                Point pt = Point.Add(center, new Size(256, 0));
                Point pt2 = Point.Add(center, new Size(0, -256));
                graphics.DrawLine(pen, center, pt);
                graphics.DrawLine(pen, center, pt2);
            }
        }

        private void DrawValues() {
            int num = center.X + 1;
            Point empty = Point.Empty;
            Point empty2 = Point.Empty;
            Color c = Color.Black;
            for (int i = 0; i < 256; i++) {
                int num2 = (int)((double)colorBucket[i] / highestValue * 256.0);
                if (color == Color.Red) {
                    c = Color.FromArgb(i, 0, 0);
                } else if (color == Color.Green) {
                    c = Color.FromArgb(0, i, 0);
                } else if (color == Color.Blue) {
                    c = Color.FromArgb(0, 0, i);
                }
                empty = new Point(num + i, center.Y - 1);
                empty2 = new Point(num + i, center.Y - 1 - num2);
                DrawValue(empty, empty2, c);
            }
        }

        private void DrawValue(Point bottom, Point top, Color c) {
            using (Graphics graphics = Graphics.FromImage(canvas)) {
                Pen pen = new Pen(c);
                graphics.DrawLine(pen, bottom, top);
            }
        }

        private Bitmap BlankBitmap(int width, int height) {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                Rectangle rect = new Rectangle(0, 0, width, height);
                graphics.FillRectangle(Brushes.White, rect);
            }
            return bitmap;
        }

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            PictureBox = new PictureBox();
            ((ISupportInitialize)PictureBox).BeginInit();
            base.SuspendLayout();
            PictureBox.BackColor = Color.White;
            PictureBox.Dock = DockStyle.Fill;
            PictureBox.Location = new Point(0, 0);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(284, 266);
            PictureBox.TabIndex = 0;
            PictureBox.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(284, 266);
            base.Controls.Add(PictureBox);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximumSize = new Size(300, 300);
            MinimumSize = new Size(300, 300);
            base.Name = "ViewHistogram";
            Text = "Histogram";
            ((ISupportInitialize)PictureBox).EndInit();
            base.ResumeLayout(false);
        }
    }

}
