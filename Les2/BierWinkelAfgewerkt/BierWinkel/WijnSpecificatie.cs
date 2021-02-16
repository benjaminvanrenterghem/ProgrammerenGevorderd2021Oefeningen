namespace BierWinkel
{
    public class WijnSpecificatie : DrankSpecificatie
    {
        #region Properties
        public WijnKleur? Kleur { get; set; } // kan null zijn: het vraagteken duidt dit aan
        #endregion

        #region Methods
        public override bool VoldoetAanSpecificatie(DrankSpecificatie specificatie)
        {
            if (specificatie.GetType() != typeof(WijnSpecificatie)) return false;
            if (((WijnSpecificatie)specificatie).Kleur != null && ((WijnSpecificatie)specificatie).Kleur != this.Kleur) return false;
            return base.VoldoetAanSpecificatie(specificatie);
        }

        public override string ToString()
        {
            return $"{Kleur}, {base.ToString()}";
        }
        #endregion
    }
}
