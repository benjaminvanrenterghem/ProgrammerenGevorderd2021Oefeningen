using System;
using System.Collections.Generic;

namespace BierWinkel
{
    public class Inventaris
    {
        #region Properties
        public Dictionary<string, Drank> Dranken { get; set; } = new Dictionary<string, Drank>();
        #endregion

        #region Methods
        public void VoegDrankToe(Wijn wijn)
        {
            if (!Dranken.ContainsKey(wijn.Naam)) Dranken.Add(wijn.Naam, wijn);
        }
        public void VoegDrankToe(Bier bier)
        {
            if (!Dranken.ContainsKey(bier.Naam)) Dranken.Add(bier.Naam, bier);
        }

        /*
        private void VoegDrankToe(double prijsPerStuk, string naam, DrankSpecificatie drankSpecificatie, SetGrootte minimumHoeveelheid)
        {
            var drank = new Drank(prijsPerStuk, naam, minimumHoeveelheid) { DrankSpecificatie = drankSpecificatie };
            if (!Dranken.ContainsKey(naam)) Dranken.Add(naam, drank);
        }
        */

        public Drank SelecteerDrank(string naam)
        {
            if (Dranken.ContainsKey(naam)) // altijd eerst testen of key erin zit vooraleer een Dictionary item terug te geven
                return Dranken[naam]; // NIET (!) gewoon: return Dranken[naam];
            return null;
        }

        public List<Drank> ZoekDrank(DrankSpecificatie specificatie)
        {
            var gevondenDranken = new List<Drank>();
            foreach (var d in Dranken.Values) // we kijken niet naar de keys, maar enkel naar ALLE waarden
            {
                // TIP: om info te krijgen in Visual Studio bij het debuggen, zie Output tab, Show output from: Debug
                System.Diagnostics.Debug.WriteLine(d.GetType() + ": " + d.ToString());
                if (d.DrankSpecificatie.VoldoetAanSpecificatie(specificatie))
                    gevondenDranken.Add(d);
            }
            return gevondenDranken;
        }
        #endregion
    }
}
