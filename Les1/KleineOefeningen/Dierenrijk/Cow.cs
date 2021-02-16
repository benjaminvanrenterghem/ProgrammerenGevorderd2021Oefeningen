using System;

namespace KleineOefeningen
{
    public class Cow: Mammal
    {
        public int StainCount { get; set; }

        public override void ToonInfo()
        {
            Console.WriteLine($"Stain count: {StainCount}");
        }
    }
}
