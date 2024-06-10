namespace RepreTarea
{
    public partial class Form1 : Form
    {
        static Bitmap bmp;
        int x, y;
        int angle;
        static Graphics g;
        PointF a, b, c, d, aa, bb, cc, dd;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            PCT_CANVAS.Image = bmp;
            g = Graphics.FromImage(bmp);

            x = PCT_CANVAS.Width / 2;
            y = PCT_CANVAS.Height / 2;

            g.TranslateTransform(x, y); // Center the origin at the center of the canvas

            // Initialize points for red and blue lines
            a = new PointF(0, -y);
            b = new PointF(0, y);
            c = new PointF(-x, 100);
            d = new PointF(x, 0);

            // Draw the red and blue lines
            g.DrawLine(Pens.Blue, a, b);
            g.DrawLine(Pens.Red, c, d);

            // Initialize points for the figure
            aa = new PointF(0, 0);
            bb = new PointF(0, -100);
            cc = new PointF(100, -100);
            dd = new PointF(100, 0);

            RenderInitialFigure();
        }

        private void RenderButton_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(angleTextBox.Text, out int parsedAngle))
            {
                angle = parsedAngle;
                RenderRotatedFigure();
            }
            else
            {
                MessageBox.Show("Please enter a valid angle.");
            }
        }

        private void RenderInitialFigure()
        {
            g.DrawLine(Pens.Gray, aa, bb);
            g.DrawLine(Pens.Gray, bb, cc);
            g.DrawLine(Pens.Gray, cc, dd);
            g.DrawLine(Pens.Gray, dd, aa);
            PCT_CANVAS.Invalidate();
        }

        private void RenderRotatedFigure()
        {
            // Clear the figure area only (not the entire canvas)
            g.Clear(Color.Transparent);

            // Draw the fixed red and blue lines
            g.DrawLine(Pens.Blue, a, b);
            g.DrawLine(Pens.Red, c, d);

            // Calculate the centroid of the figure
            PointF centroid = CalculateCentroid(new List<PointF> { aa, bb, cc, dd });

            // Rotate each point around the centroid
            PointF rotatedA = RotateAroundPoint(aa, centroid, angle);
            PointF rotatedB = RotateAroundPoint(bb, centroid, angle);
            PointF rotatedC = RotateAroundPoint(cc, centroid, angle);
            PointF rotatedD = RotateAroundPoint(dd, centroid, angle);

            // Draw the rotated figure
            g.DrawLine(Pens.Gray, rotatedA, rotatedB);
            g.DrawLine(Pens.Gray, rotatedB, rotatedC);
            g.DrawLine(Pens.Gray, rotatedC, rotatedD);
            g.DrawLine(Pens.Gray, rotatedD, rotatedA);

            PCT_CANVAS.Invalidate();
        }

        private PointF RotateAroundPoint(PointF point, PointF pivot, int angle)
        {
            double radians = angle * Math.PI / 180.0;
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            float dx = point.X - pivot.X;
            float dy = point.Y - pivot.Y;

            return new PointF
            (
                cosTheta * dx - sinTheta * dy + pivot.X,
                sinTheta * dx + cosTheta * dy + pivot.Y
            );
        }

        private PointF CalculateCentroid(List<PointF> points)
        {
            float sumX = 0, sumY = 0;
            foreach (var point in points)
            {
                sumX += point.X;
                sumY += point.Y;
            }
            return new PointF(sumX / points.Count, sumY / points.Count);
        }

        private void RenderLine(PointF a, PointF b)
        {
            g.DrawLine(Pens.Gray, TranslateToCenter(a), TranslateToCenter(b));
        }

        private PointF TranslateToCenter(PointF a)
        {
            int Sx = (bmp.Width / 2);
            int Sy = (bmp.Height / 2);
            return new PointF(Sx + a.X, Sy - a.Y);
        }
    }
}
