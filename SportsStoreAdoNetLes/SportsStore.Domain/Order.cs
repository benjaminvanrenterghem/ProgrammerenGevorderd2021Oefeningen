using System;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Interfaces;

namespace SportsStore.Domain
{
    public class Order: IPersist
    {
        #region Fields
        private Contracts.Order _data;
        private bool _isDirty;
        #endregion

        #region Properties
        public Contracts.Order Data { get { return _data; } }
        public int OrderId { get { return _data.OrderId; } set { if(_data.OrderId != value){ _data.OrderId = value; _isDirty = true; } } }
        public int CartId { get { return _data.CartId; } set { if(_data.CartId != value) { _data.CartId = value; _isDirty = true; } } }
        public DateTimeOffset OrderDate { get { return _data.OrderDate; } set { if(_data.OrderDate != value){ _data.OrderDate = value; _isDirty = true; } } }
        public DateTimeOffset DeliveryDate { get { return _data.DeliveryDate; } set { if (_data.DeliveryDate != value) { _data.DeliveryDate = value; _isDirty = true; } } }
        public bool GiftWrapping { get { return _data.GiftWrapping; } set { if (_data.GiftWrapping != value) { _data.GiftWrapping = value; _isDirty = true; } } }
        public string ShippingStreet { get { return _data.ShippingStreet; } set { if (_data.ShippingStreet != value) { _data.ShippingStreet = value; _isDirty = true; } } }

        private City _shippingCity;
        public City ShippingCity { get { return _shippingCity; } set { if (_shippingCity.Id != value.Id) { _shippingCity = value; _data.ShippingCity = value.Data; _isDirty = true; } } }

        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        #endregion

        #region Ctor
        public Order()
        {
            _isDirty = true;
            _data = new Contracts.Order();
        }

        public Order(Contracts.Order order)
        {
            _data = order;
            if (DAL.Order.Exists(order))
                Load();
            else
                _isDirty = true;
        }

        public Order(Cart cart, DateTime deliveryDate, bool giftwrapping, string shippingStreet, Contracts.City shippingCity)
    : this()
        {
            if (!cart.CartLines.Any())
                throw new InvalidOperationException("Cannot place order when cart is empty");

            foreach (CartLine line in cart.CartLines)
                OrderLines.Add(new OrderLine
                {
                    Product = line.Product,
                    Price = line.Product.Price,
                });

            _data.OrderDate = DateTime.Today;
            _data.DeliveryDate = deliveryDate;
            _data.GiftWrapping = giftwrapping;
            _data.ShippingStreet = shippingStreet;
            _data.ShippingCity = shippingCity;
        }
        #endregion

        #region Methods
        #region IPersist
        public void Load()
        {
            if (_data.OrderId == 0) return;
            DAL.Order.Select(_data);
        }      
        
        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.Order.Exists(_data))
                {
                    DAL.Order.Insert(_data);
                }
                else
                {
                    DAL.Order.Update(_data);
                }
            }
        }
        #endregion

        public bool HasOrdered(Contracts.Product p) => OrderLines.Any(l => l.Product.Equals(p));
        #endregion

    }
}
