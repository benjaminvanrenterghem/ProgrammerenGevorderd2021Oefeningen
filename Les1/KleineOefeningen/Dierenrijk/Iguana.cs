using System;

namespace KleineOefeningen
{
    public class Iguana: Reptile
    {
        public bool IsPolyColor { get; set; }

        public override void ToonInfo()
        {
            Console.WriteLine($"Is veelkleurig: {IsPolyColor}");
        }
    }
}
