using System;

namespace KleineOefeningen
{
    public class Reptile : Animal
    {
        public int ScaleCount { get; set; } = 100;

        public override void ToonInfo()
        {
            Console.WriteLine($"Aantal schubben: {ScaleCount}");
        }
    }
}
