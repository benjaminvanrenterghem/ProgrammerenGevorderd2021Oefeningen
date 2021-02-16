using System;

namespace BierWinkel
{
    public class Wijn: Drank
    {
        #region Ctor
        public Wijn(double prijsPerStuk, string naam, WijnSpecificatie wijnSpecificatie, SetGrootte minimumHoeveelheid)
            : base(prijsPerStuk, naam, minimumHoeveelheid)
        {
            // Precondities
            if (prijsPerStuk <= 0) throw new Exception("Prijs moet groter zijn dan 0");
            if (string.IsNullOrEmpty(naam)) throw new Exception("Naam wijn moet gekend zijn");

            DrankSpecificatie = wijnSpecificatie;
        }
        #endregion
    }
}
