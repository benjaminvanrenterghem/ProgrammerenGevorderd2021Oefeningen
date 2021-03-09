using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Order
    {
        #region Properties
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Giftwrapping { get; set; }
        public string ShippingStreet { get; set; }
        public City ShippingCity { get; set; }
        public ICollection<OrderLine> OrderLines { get; private set; }
        public decimal Total => OrderLines.Sum(o => o.Price * o.Quantity);
        #endregion

        #region Constructors
        public Order() => OrderLines = new HashSet<OrderLine>();

        public Order(Cart cart, DateTime? deliveryDate, bool giftwrapping, string shippingStreet, City shippingCity)
            : this()
        {
            if (!cart.CartLines.Any())
                throw new InvalidOperationException("Cannot place order when cart is empty");

            foreach (CartLine line in cart.CartLines)
                OrderLines.Add(new OrderLine
                {
                    Product = line.Product,
                    Price = line.Product.Price,
                    Quantity = line.Quantity
                });

            OrderDate = DateTime.Today;
            DeliveryDate = deliveryDate;
            Giftwrapping = giftwrapping;
            ShippingStreet = shippingStreet;
            ShippingCity = shippingCity;
        }
        #endregion

        #region Methods
        public bool HasOrdered(Product p) => OrderLines.Any(l => l.Product.Equals(p));
        #endregion

    }
}