namespace BierWinkel
{
    public class Bier
    {
        #region Properties
        public double PrijsPerStuk { get; set; }
        public string Naam { get; set; }
        public int MinimumHoeveelheid { get; set; }

        public string Kleur { get; set; }
        public string Brouwerij { get; set; }
        public double Volume { get; set; }
        public double AlcoholPercentage { get; set; }
        #endregion

        #region Ctor
        public Bier(double prijsPerStuk, string naam, string kleur, string brouwerij, double volume, double alcoholPercentage, int minimumHoeveelheid)
        {
            //if (prijsPerStuk <= 0) throw new Exception("Prijs moet groter zijn dan 0");
            PrijsPerStuk = prijsPerStuk;

            //if (string.IsNullOrEmpty(naam)) throw new Exception("Naam bier moet gekend zijn");
            Naam = naam;

            Kleur = kleur;
            Brouwerij = brouwerij;
            Volume = volume;
            AlcoholPercentage = alcoholPercentage;

            MinimumHoeveelheid = minimumHoeveelheid;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{PrijsPerStuk}, {Naam}, {Kleur}, {Brouwerij}, {Volume}, {AlcoholPercentage}, {MinimumHoeveelheid}";
        }
        #endregion
    }
}
