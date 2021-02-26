using System;
using System.Collections.Generic;
using WinkelManagement.Events;

namespace WinkelManagement
{
    public class Stockbeheer
    {
        #region Properties
        private const int MINIMUM_AANTAL = 25;  // grens minimum stock
        private const int MAXIMUM_AANTAL = 100; // grens maximum stock
        private Dictionary<ProductType, int> _stock;
        #endregion

        #region Events
        public event EventHandler<StockbeheerEventArgs> StockOnderMinimum;  // Event
        #endregion

        #region Ctor
        public Stockbeheer()
        {
            _stock = new Dictionary<ProductType, int>();
            foreach (ProductType type in Enum.GetValues(typeof(ProductType)))
            {
                _stock[type] = 100;
            }
        }
        #endregion

        #region Methods
        public void OnProductVerkocht(object sender, WinkelEventArgs args)  // Event Handler
        {
            _stock[args.Bestelling.ProductType] -= args.Bestelling.Aantal;
            if (_stock[args.Bestelling.ProductType] < MINIMUM_AANTAL)
            {
                int teBestellen = MAXIMUM_AANTAL - _stock[args.Bestelling.ProductType];
                OnStockOnderMinimum(args.Bestelling.ProductType, teBestellen);
            }
        }

        public void OnBestellingGeplaatst(object sender, StockbeheerEventArgs args)  // Event Handler
        {
            _stock[args.Type] += args.Aantal;
        }

        protected virtual void OnStockOnderMinimum(ProductType type, int aantal)    // Event Init
        {
            StockOnderMinimum?.Invoke(this, new StockbeheerEventArgs(type, aantal));
        }

        public void ShowStock()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("STOCKOVERZICHT");
            foreach (var item in _stock)
            {
                Console.WriteLine($"Stock: {item.Key}, {item.Value}");
            }
            Console.WriteLine("--------------------------");
        }
        #endregion
    }
}
