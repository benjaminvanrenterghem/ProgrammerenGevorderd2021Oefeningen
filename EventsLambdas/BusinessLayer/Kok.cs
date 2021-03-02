// HOGENT
using BusinessLayer.Events;
using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class Kok // consumer van een event: abonneert zich op een event, namelijk op die van het bestellingssysteem
    {
        #region Properties

        public string Naam { get; set; }

        private BestellingsSysteem _bestellingsSysteem;
        public BestellingsSysteem BestellingsSysteem
        {
            get
            {
                return _bestellingsSysteem;
            }
            set
            {
                // preconditie: ik zorg ervoor dat ik me maar eenmaal inschrijf op een event
                if (_bestellingsSysteem != null) _bestellingsSysteem.BestellingEvent -= OnBestelling; // uitschrijven indien nodig
                _bestellingsSysteem = value;
                _bestellingsSysteem.BestellingEvent += OnBestelling; // als de bestelling event opgeroepen wordt, dan wordt method BestellingOntvangen
            }
        }

        // Event:
        // ------
        public Bel Bel { get; set; }
        // Interface:
        // ----------
        //public IBel Bel { get; set; }


        #endregion

        #region Ctor
        public Kok(string naam)
        {
            Naam = naam;
        }
        #endregion

        #region Method
        public void OnBestelling(object sender, BestelEventArgs args)
        {
            if (args == null || string.IsNullOrEmpty(args.ProductNaam)) return; // preconditie

            System.Diagnostics.Debug.WriteLine(args.ProductNaam + " in voorbereiding");
            System.Threading.Thread.Sleep(5000); // ik slaap 5 seconden: 5 keer 1000 milliseconden
            // Event:
            // ------
            Bel.Ring(args); // losse koppeling: kok weet niet dat het de ober is die op het event geabonneerd is
            // Interface:
            // ----------
            //if (Bel != null) Bel.Ring(this, args);
        }
        #endregion
    }
}
