namespace BierWinkel2
{
    public class BierSpecificatie
    {
        public BierKleur? Kleur { get; set; }
        public string Brouwerij { get; set; }
        public BierVolume? Volume { get; set; }
        public double? AlcoholPercentage { get; set; }
        public string HerkomstLand { get; set; }

        public bool VoldoetAanSpecificatie(BierSpecificatie bierSpecificatie)
        {
            if (bierSpecificatie.Kleur != null && bierSpecificatie.Kleur != this.Kleur) return false;
            if (bierSpecificatie.Brouwerij != null && bierSpecificatie.Brouwerij.Length > 0 && bierSpecificatie.Brouwerij.ToLower() != this.Brouwerij.ToLower()) return false;
            if (bierSpecificatie.Volume != null && bierSpecificatie.Volume != this.Volume) return false;
            if (bierSpecificatie.AlcoholPercentage != null && bierSpecificatie.AlcoholPercentage != this.AlcoholPercentage) return false;
            if (bierSpecificatie.HerkomstLand != null && bierSpecificatie.HerkomstLand != this.HerkomstLand) return false;
            return true;
        }

        public override string ToString()
        {
            return $"{Kleur}, {Brouwerij}, {Volume}, {AlcoholPercentage}";
        }
    }
}
