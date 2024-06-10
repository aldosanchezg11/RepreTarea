using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepreTarea
{
    public class Figure
    {
        public PointF centroid;
        public List<PointF> pts;

        public Figure(List<PointF> pts)
        {
            if (pts == null || pts.Count == 0)
            {
                throw new ArgumentException("Points list cannot be null or empty", nameof(pts));
            }

            this.pts = pts; // initialize the pts field

            centroid = new PointF();
            for (int p = 0; p < pts.Count; p++)
            {
                centroid.X += pts[p].X;
                centroid.Y += pts[p].Y;
            }
            centroid.X /= pts.Count;
            centroid.Y /= pts.Count;
        }
    }
}

