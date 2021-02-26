using System;

namespace SegmentNs
{
    public static class SegmentExtension
    {
        public static double Length(this Segment s)
        {
            var l = 0.0;

            for (var i = 1; i < s.Points.Count; i++)
            {
                l += Math.Sqrt(Math.Pow(s.Points[i].X - s.Points[i - 1].X, 2) + Math.Pow(s.Points[i].Y - s.Points[i - 1].Y, 2));
            }
            return l;
        }
    }
}
