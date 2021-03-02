// HOGENT
using BusinessLayer.Events;
using BusinessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Ober: IBel
    {
        #region Properties
        private List<Klant> _klanten = new(); // C# 9.0: je kan enkel new() gebruiken, in plaats van new List<Klant>();

        public string Naam { get; set; }

        public BestellingsSysteem BestellingsSysteem { get; set; }

        // Event:
        // ------        
        private Bel _bel;

        public Bel Bel
        {
            get
            {
                return this._bel;
            }

            set
            {
                if (this._bel != null) this._bel.RingEvent -= this.OnRing; // goed zien dat je je niet op 2 belletjes of 2 keer op hetzelfde belletje inschrijft!!
                this._bel = value;
                this._bel.RingEvent += this.OnRing;
            }
        }

        #endregion

        #region Ctor
        public Ober(string name)
        {
            this.Naam = name;
        }
        #endregion

        #region Methods
        public void BrengBestelling(Klant klant, string product)
        {
            if (klant == null || string.IsNullOrEmpty(product)) return; // preconditie

            if (!_klanten.Contains(klant))
                _klanten.Add(klant);

            BestellingsSysteem.GeefBestellingIn(new BestelEventArgs { KlantNaam = klant.Naam, ProductNaam = product });
        }

        private void OnRing(object sender, BestelEventArgs args)
        {
            // LINQ: sneak preview; vergelijk SQL, declaratief - niet iteratief
            var klant = this._klanten.Where(k => k.Naam == args.KlantNaam).FirstOrDefault(); // EERSTE OF null want klant is object (default is dan null)
            if (klant == null) return;

            klant.Betaal(args.ProductNaam);
            klant.Consumeer(args.ProductNaam);
        }

        // Interface:
        // ----------
        #region Interface INotify
        public void Ring(object sender, BestelEventArgs args)
        {
            OnRing(sender, args);
        }
        #endregion
        #endregion
    }
}
