using System;

namespace Vaccination
{
    public class Vaccin
    {
        #region Properties
        public string Naam { get; private set; }
        public int Oplossing { get; set; } = -1;

        private static Random _r = new Random();
        #endregion

        #region Ctor
        public Vaccin(string naam, int o = -1)
        {
            Naam = naam;
            Oplossing = o;
        }
        #endregion

        #region Methods
        public int TryKillCode()
        {
            if (Oplossing != -1)
            {
                return Oplossing;
            }

            return _r.Next(1, 100);
        }

        public void ToonInfo()
        {
            Console.WriteLine($"{Naam}, oplossing is: {Oplossing}");
        }
        #endregion
    }
}
