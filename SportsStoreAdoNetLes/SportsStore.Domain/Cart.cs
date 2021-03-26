using SportsStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain
{

    /// <summary>
    /// Cart: winkelkar; CartLine: product in winkelkar
    /// </summary>
    public class Cart: IPersist
    {
        #region Fields
        private Contracts.Cart _data;
        private bool _isDirty = false;
        #endregion

        #region Properties
        //public Contracts.Cart Data { get { return _data; } }
        public int Id { get => _data.Id; set { if (_data.Id != value) { _data.Id = value; _isDirty = true; } } }
        public DateTimeOffset ShoppingDate { get { return _data.ShoppingDate; } set { if (_data.ShoppingDate != value) { _data.ShoppingDate = value; _isDirty = true; } } }

        public List<CartLine> CartLines { get; set; } = new List<CartLine>();

        // Property, maar met lambda (verkorte notatie van de get): bij enkel get en lambda kan get ook nog weg:
        //public int NumberOfItems => CartLines.Count;
        public int NumberOfItems { get => CartLines.Count; set { } }
        //public int NumberOfItems { get { return CartLines.Count; } }
        // set nodig? Dan echt uitschrijven met get, hoewel get nog met lambda kan werken

        // Property, met lambda, en ... met LINQ (Sum is een LINQ extension method):
        public decimal TotalValue => CartLines.Sum(l => l.Product.Price * l.Quantity);
        #endregion

        #region Ctor
        public Cart()
        {
            _isDirty = true;
            _data = new Contracts.Cart
            {
                ShoppingDate = DateTimeOffset.Now
            };
        }

        public Cart(Contracts.Cart cart)
        {
            _data = cart;
            if (DAL.Cart.Exists(cart))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Methods
        public void AddLine(Product product, int quantity)
        {
            // met LINQ zoeken we of er al producten van dit type in onze winkelkar zitten; zitten er geen in, dan krijgen we null terug, wat de "default" is van een class
            var line = CartLines.SingleOrDefault(l => l.Product.Equals(product)); // SingleOrDefault: 1 of geen en als geen, dan default, dus null
            if (line == null)
                CartLines.Add(new CartLine { Product = product, Quantity = quantity });
            else line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            CartLine line = CartLines.SingleOrDefault(l => l.Product.Equals(product));
            if (line != null)
                CartLines.Remove(line);
        }

        public void Clear() => CartLines.Clear();

        #region interface IPersist
        public void Load()
        {
            if (_data.Id == 0) return;
            DAL.Cart.Select(_data); // hier roepen we DAL laag op
        }  
        
        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.Cart.Exists(_data))
                {
                    DAL.Cart.Insert(_data);
                }
                else
                {
                    DAL.Cart.Update(_data);
                }
            }
        }
        #endregion
        #endregion
    }
}
