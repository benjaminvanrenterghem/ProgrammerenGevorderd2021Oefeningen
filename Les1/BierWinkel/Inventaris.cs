using System.Collections.Generic;
//using System.Linq;

namespace BierWinkel
{
    public class Inventaris
    {
        #region Properties
        public List<Bier> Biertjes { get; set; } = new List<Bier>();
        #endregion

        #region Ctor
        public void VoegBierToe(double prijsPerStuk, string naam, string kleur, string brouwerij, double volume, double alcoholPercentage, int minimumHoeveelheid)
        {
            var bier = new Bier(prijsPerStuk, naam, kleur, brouwerij, volume, alcoholPercentage, minimumHoeveelheid);
            Biertjes.Add(bier);
        }
        #endregion

        #region Methods
        public Bier SelecteerBier(string naam)
        {
            //return Biertjes.SingleOrDefault<Bier>(b => b.Naam == naam);
            foreach (Bier bier in Biertjes)
            {
                if (bier.Naam == naam) return bier;
            }
            return null;
        }

        public Bier ZoekBier(Bier bier)
        {
            List<Bier> gevondenBiertjes = new List<Bier>();
            foreach (Bier b in Biertjes)
            {
                if (bier.Kleur.Length > 0 && bier.Kleur != b.Kleur) continue;
                if (bier.Brouwerij != null && bier.Brouwerij.Length > 0 && bier.Brouwerij != b.Brouwerij) continue;
                if (bier.Volume > 0 && bier.Volume != b.Volume) continue;
                if (bier.AlcoholPercentage >= 0 && bier.AlcoholPercentage != b.AlcoholPercentage) continue;
                return b;
            }
            return null;
        }
        #endregion
    }
}
