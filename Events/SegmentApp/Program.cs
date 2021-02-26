using SegmentNs;
using System;
using System.Collections.Generic;

namespace SegmentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var xc = new List<int> { 0, 5, 10 };
            var yc = new List<int> { 0, 5, 10 };
            Console.WriteLine($"Segmentlengte: {new Segment(xc, yc).Length()}");
        }
    }
}
