using System;

namespace KleineOefeningen
{
    public class Snake: Reptile
    {
        public bool CanCurl { get; set; } = true;

        public override void ToonInfo()
        {
            Console.WriteLine($"Kan kronkelen: {CanCurl}");
        }
    }
}
