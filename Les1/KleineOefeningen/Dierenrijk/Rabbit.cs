using System;

namespace KleineOefeningen
{
    public class Rabbit: Mammal
    {
        public bool CanJump { get; set; }

        public override void ToonInfo()
        {
            //base.ToonInfo();
            Console.WriteLine($"Kan bewegen: {CanMove}, aantal haren: {HairCount}, kan springen: {CanJump}");
        }
    }
}
