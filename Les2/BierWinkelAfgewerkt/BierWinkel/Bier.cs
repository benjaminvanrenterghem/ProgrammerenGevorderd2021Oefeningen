using System;

namespace BierWinkel
{
    public class Bier : Drank
    {
        #region Ctor
        public Bier(double prijsPerStuk, string naam, BierSpecificatie bierSpecificatie, SetGrootte minimumHoeveelheid)
            : base(prijsPerStuk, naam, minimumHoeveelheid)
        {
            // Precondities
            if (prijsPerStuk <= 0) throw new Exception("Prijs moet groter zijn dan 0");
            if (string.IsNullOrEmpty(naam)) throw new Exception("Naam bier moet gekend zijn");

            DrankSpecificatie = bierSpecificatie;
        }
        #endregion
    }
}
