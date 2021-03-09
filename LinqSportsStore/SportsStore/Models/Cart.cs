using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Cart
    {
        #region Properties
        private readonly IList<CartLine> _lines = new List<CartLine>();
        public IEnumerable<CartLine> CartLines
        {
            get
            {
                return _lines.AsEnumerable();
            }
        }

        public int NumberOfItems
        {
            get
            {
                return _lines.Count;
            }
        }

        public decimal TotalValue
        {
            get { return _lines.Sum(l => l.Product.Price * l.Quantity); }
        }
        #endregion

        #region Methods
        public void AddLine(Product product, int quantity)
        {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line == null)
                _lines.Add(new CartLine { Product = product, Quantity = quantity });
            else line.Quantity += quantity;

        }

        public void RemoveLine(Product product)
        {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line != null)
                _lines.Remove(line);
        }

        public void Clear()
        {
            _lines.Clear();
        }

        #endregion
    }
}