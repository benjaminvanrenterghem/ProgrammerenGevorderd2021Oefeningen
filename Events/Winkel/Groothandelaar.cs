using System;
using System.Collections.Generic;
using WinkelManagement.Events;

namespace WinkelManagement
{
    public class Groothandelaar
    {
        #region Events
        public event EventHandler<StockbeheerEventArgs> BestellingGeplaatst;
        #endregion

        #region Properties
        private List<Bestelling> _bestellingen;
        #endregion

        #region Ctor
        public Groothandelaar()
        {
            _bestellingen = new List<Bestelling>();
        }
        #endregion

        #region Methods
        public void OnStockOnderMinimum(object sender, StockbeheerEventArgs args)
        {
            var b = new Bestelling(args.Type, args.Aantal);
            _bestellingen.Add(b);
            OnBestellingGeplaatst(b);
        }

        protected virtual void OnBestellingGeplaatst(Bestelling b)
        {
            BestellingGeplaatst?.Invoke(this, new StockbeheerEventArgs(b.ProductType, b.Aantal));
        }

        public void ShowLaatsteBestelling()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("LAATSTE BESTELLING");
            if (_bestellingen.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen bestellingen geplaatst.");
            }
            else
            {
                Console.WriteLine($"Laatste bestelling: {_bestellingen[^1].ProductType}, {_bestellingen[^1].Aantal}");
            }
            Console.WriteLine("--------------------------");
        }

        public void ShowAlleBestellingen()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("BESTELLINGEN GROOTHANDEL");
            if (_bestellingen.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen bestellingen geplaatst.");
            }
            foreach (var bestelling in _bestellingen)
            {
                Console.WriteLine($"Voorraadbestelling: {bestelling.ProductType}, {bestelling.Aantal}");
            }
            Console.WriteLine("--------------------------");
        }
        #endregion
    }
}
