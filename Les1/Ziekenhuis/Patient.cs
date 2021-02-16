using System;

namespace Veearts
{
    public abstract class Dier
    {
        public int Name { get; set; }

        public abstract string MaakGeluid(); // kan geen implementatie
    }

    public class Paard: Dier
    {
        public override string MaakGeluid() { return "Hinnik"; }
    }

    public class Wolf: Dier
    {
        public override string MaakGeluid() { return "Oeoe"; }
    }
}

namespace Ziekenhuis
{

    public class Patient
    {
        #region Properties
        public string Naam { get; set; }
        public int UrenInZiekenhuis { get; set; }

        private const int _basisKost = 50;
        private const int _kostPerUur = 20;
        #endregion

        #region Methods
        public virtual double BerekenKost()
        {
            var kost = _basisKost + (UrenInZiekenhuis * _kostPerUur);
            return kost;
        }

        public void ToonInfo()
        {
            Console.WriteLine($"{Naam} (Kost: {BerekenKost()})");
        }
        #endregion
    }
}
