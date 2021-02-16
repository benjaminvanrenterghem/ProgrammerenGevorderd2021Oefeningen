using System;

namespace BierWinkel2
{
    public class Bier: Drank
    {
        public Bier(double prijsPerStuk, string naam, BierSpecificatie bierSpecificatie, int minimumHoeveelheid)
        {
            if (prijsPerStuk <= 0) throw new Exception("Prijs moet groter zijn dan 0");
            PrijsPerStuk = prijsPerStuk;

            if (string.IsNullOrEmpty(naam)) throw new Exception("Naam bier moet gekend zijn");
            Naam = naam;

            BierSpecificatie = bierSpecificatie;
            MinimumHoeveelheid = minimumHoeveelheid;
        }

       public BierSpecificatie BierSpecificatie { get; set; }

        public override string ToString()
        {
            return $"{PrijsPerStuk}, {Naam}, {BierSpecificatie}, {MinimumHoeveelheid}";
        }
    }
}
