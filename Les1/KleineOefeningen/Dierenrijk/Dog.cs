using System;

namespace KleineOefeningen
{
    public class Dog: Mammal
    {
        public bool CanBark { get; set; }
        public override void ToonInfo()
        {
            Console.WriteLine($"Kan blaffen: {CanBark}");
        }
    }
}
