using System;

namespace KleineOefeningen
{
    public class Mammal : Animal
    {
        public int HairCount { get; set; } = 100000;

        public override void ToonInfo()
        {
            //base.ToonInfo();
            Console.WriteLine($"Aantal haren: {HairCount} ({_weight})");
        }
    }
}
