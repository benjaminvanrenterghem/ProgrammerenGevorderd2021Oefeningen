using System;

namespace KleineOefeningen
{
    public class Vliegtuig
    {
        public void Start()
        {
            Console.WriteLine("Starting plane");
        }

        public virtual void Vlieg()
        {
            Console.WriteLine("Flying");
        }
    }
}
