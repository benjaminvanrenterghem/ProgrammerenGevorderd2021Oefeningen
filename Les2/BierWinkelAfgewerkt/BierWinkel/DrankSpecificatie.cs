namespace BierWinkel
{
    public class DrankSpecificatie
    {
        #region Properties
        public string Brouwerij { get; set; }
        public Volume? Volume { get; set; }
        public double? AlcoholPercentage { get; set; }
        public string HerkomstLand { get; set; }
        #endregion

        #region Methods
        public virtual bool VoldoetAanSpecificatie(DrankSpecificatie specificatie)
        {
            if (specificatie.Brouwerij != null && specificatie.Brouwerij.Length > 0 && specificatie.Brouwerij.ToLower() != this.Brouwerij.ToLower()) return false;
            if (specificatie.Volume != null && specificatie.Volume != this.Volume) return false;
            if (specificatie.AlcoholPercentage != null && specificatie.AlcoholPercentage != this.AlcoholPercentage) return false;
            if (specificatie.HerkomstLand != null && specificatie.HerkomstLand != this.HerkomstLand) return false;
            return true;

        }
        public override string ToString()
        {
            return $"{Brouwerij}, {Volume}, {AlcoholPercentage}, {HerkomstLand}";
        }
        #endregion
    }
}
