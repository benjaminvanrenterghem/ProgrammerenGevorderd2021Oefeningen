using System;
using System.Collections.Generic;
using System.Linq;
using WinkelManagement.Events;

namespace WinkelManagement
{
    public class Sales
    {
        #region Properties
        private Dictionary<string, List<Bestelling>> _rapport;
        #endregion

        #region Ctor
        public Sales()
        {
            _rapport = new Dictionary<string, List<Bestelling>>();
        }
        #endregion

        #region Methods
        public void OnProductVerkocht(object sender, WinkelEventArgs args)  // Event Handler
        {
            if (_rapport.ContainsKey(args.Bestelling.Adres))
            {
                _rapport[args.Bestelling.Adres].Add(args.Bestelling);
            }
            else
            {
                List<Bestelling> bestellingen = new List<Bestelling> { args.Bestelling };
                _rapport[args.Bestelling.Adres] = bestellingen;
            }
        }

        public void ShowRapport()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("SALESRAPPORT");
            foreach (var verkoop in _rapport)
            {
                Console.WriteLine(verkoop.Key);
                var query = verkoop.Value.GroupBy(
                    x => x.ProductType,
                    (type, bestelling) => new
                    {
                        Type = type,
                        Aantal = bestelling.Sum(x => x.Aantal)
                    });
                foreach (var bestelling in query)
                {
                    Console.WriteLine($"    {bestelling.Type}, {bestelling.Aantal}");
                }
            }
            Console.WriteLine("--------------------------");
        }
        #endregion
    }
}
