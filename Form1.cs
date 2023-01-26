namespace RepreTarea
{
    public partial class Form1 : Form
    {
        static Bitmap bmp;
        int x, y, angle;
        static Graphics g;
        Point a, b, c, d, aa, bb, cc, dd;

        

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            PCT_CANVAS.Image = bmp;
            g = Graphics.FromImage(bmp);

            x = PCT_CANVAS.Width / 2;
            y = PCT_CANVAS.Height / 2;

            g.TranslateTransform(x, y); //Change from 0, 0 to x, y

            a = new Point(0, -y);
            b = new Point(0, y);
            c = new Point(-x, 100);
            d = new Point(x, 0);

            g.DrawLine(Pens.Blue, a, b);
            g.DrawLine(Pens.Red, c, d);

            aa = new Point(0, 0);
            bb = new Point(0, -100);
            cc = new Point(100, -100);
            dd = new Point(100, 0);

            g.DrawLine(Pens.Gray, aa, bb);
            g.DrawLine(Pens.Gray, bb, cc);
            g.DrawLine(Pens.Gray, cc, dd);
            g.DrawLine(Pens.Gray, dd, aa);

            PCT_CANVAS.Invalidate();
        }

        private void Render()
        {
            g.Clear(Color.Transparent);
            g.DrawLine(Pens.Yellow, bmp.Width / 2, 0, bmp.Width / 2, bmp.Height);
            g.DrawLine(Pens.Yellow, 0, bmp.Height / 2, bmp.Width, bmp.Height / 2);

            RenderLine(a, b);
            RenderLine(b, c);
            RenderLine(c, d);
            RenderLine(d, a);
            Figure(a, b);
            PCT_CANVAS.Invalidate();

        }

        private PointF Rotate(PointF a)
        {
            PointF b = new PointF();
            b.X = (float)((a.X * Math.Cos(angle)) - (a.Y * Math.Sin(angle)));
            b.Y = (float)((a.X * Math.Sin(angle)) - (a.Y * Math.Cos(angle)));
            return b;
        }

        private PointF Translate(PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        private PointF TranslateToCenter(PointF a)
        {
            int Sx = (bmp.Width / 2);
            int Sy = (bmp.Height / 2);

            return new PointF(Sx + a.X, Sy - a.Y);
        }

        private void RenderLine(PointF a, PointF b)
        {
            a = Translate(a, new PointF(-50, -50));
            b = Translate(b, new PointF(-50, -50));

            PointF c = Rotate(a);
            PointF d = Rotate(b);

            c = TranslateToCenter(c);
            d = TranslateToCenter(d);

            g.DrawLine(Pens.Gray, c, d);
        }

    }
}