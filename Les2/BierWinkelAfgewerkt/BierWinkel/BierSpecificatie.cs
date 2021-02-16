
namespace BierWinkel
{

    public class BierSpecificatie: DrankSpecificatie
    {
        #region Properties
        public BierKleur? Kleur { get; set; } // kan null zijn: dit wordt aangegeven door het vraagteken
        #endregion

        #region Methods
        public override bool VoldoetAanSpecificatie(DrankSpecificatie specificatie)
        {
            // Een DrankSpecificatie kan een BierSpecificatie of een WijnSpecificatie zijn: ik moet dus testen of het wel een BierSpecificatie is!!
            // Van een object: .GetType() en van een class: typeof()
            if (specificatie.GetType() != typeof(BierSpecificatie)) return false;
            if (((BierSpecificatie)specificatie).Kleur != null && ((BierSpecificatie)specificatie).Kleur != this.Kleur) return false;
            return base.VoldoetAanSpecificatie(specificatie);
        }

        public override string ToString()
        {
            return $"{Kleur}, {base.ToString()}";
        }
        #endregion
    }
}
