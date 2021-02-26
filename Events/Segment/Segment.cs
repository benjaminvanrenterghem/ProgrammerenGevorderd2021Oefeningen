using System.Collections.Generic;
using System.Drawing;

namespace SegmentNs
{
    public class Segment
    {
        public List<Point> Points { get; set; } = new List<Point>();
        public Segment(List<int> xl, List<int> yl)
        {
            for(var i = 0; i < xl.Count; i++)
            {
                Points.Add(new Point(xl[i], yl[i]));
            }
        }
    }
}
