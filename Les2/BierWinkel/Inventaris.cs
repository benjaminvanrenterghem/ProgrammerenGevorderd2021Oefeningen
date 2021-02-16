using System.Collections.Generic;
//using System.Linq;

namespace BierWinkel2
{
    public class Inventaris
    {
        public Dictionary<string, Bier> Biertjes { get; set; } = new Dictionary<string, Bier>();

        public void VoegBierToe(double prijsPerStuk, string naam, BierSpecificatie bierSpecificatie, int minimumHoeveelheid)
        {
            var bier = new Bier(prijsPerStuk, naam, bierSpecificatie, minimumHoeveelheid);
            //Biertjes.Add(bier);
            if (!Biertjes.ContainsKey(naam)) Biertjes.Add(bier.Naam, bier);
        }

        public Bier SelecteerBier(string naam)
        {
            //return Biertjes.SingleOrDefault<Bier>(b => b.Naam == naam);
            foreach (Bier bier in Biertjes.Values)
            {
                if (bier.Naam == naam) return bier;
            }
            // TO DO: VERBETEREN - we hebben een Dictionary!
            return null;
        }

        public List<Bier> ZoekBier(BierSpecificatie bierSpecificatie)
        {
            List<Bier> gevondenBiertjes = new List<Bier>();
            foreach (Bier b in Biertjes.Values)
            {
                /*
                if (bier.BierSpecificatie?.Kleur != null && bier.BierSpecificatie?.Kleur != b.BierSpecificatie?.Kleur) continue;
                if (bier.BierSpecificatie?.Brouwerij != null && bier.BierSpecificatie?.Brouwerij.Length > 0 && bier.BierSpecificatie?.Brouwerij != b.BierSpecificatie?.Brouwerij) continue;
                if (bier.BierSpecificatie?.Volume > 0 && bier.BierSpecificatie?.Volume != b.BierSpecificatie?.Volume) continue;
                if (bier.BierSpecificatie?.AlcoholPercentage >= 0 && bier.BierSpecificatie?.AlcoholPercentage != b.BierSpecificatie?.AlcoholPercentage) continue;
                return b;
                */
                if (b.BierSpecificatie.VoldoetAanSpecificatie(bierSpecificatie)) gevondenBiertjes.Add(b);
            }
            return gevondenBiertjes;
        }
    }
}
