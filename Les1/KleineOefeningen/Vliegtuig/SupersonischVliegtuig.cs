using System;

namespace KleineOefeningen
{
    public class SupersonischVliegtuig : Vliegtuig
    {
        public new void Start()
        {
            Console.WriteLine("Starting supersonic plane");
        }

        public override void Vlieg()
        {
            base.Vlieg();
            Console.WriteLine("Flying supersonic");
        }
    }
}
