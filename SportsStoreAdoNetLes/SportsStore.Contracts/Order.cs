using System;
using System.Collections.Generic;

namespace SportsStore.Contracts
{
    public class Order
    {
        #region Properties
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public bool GiftWrapping { get; set; }
        public string ShippingStreet { get; set; }
        public City ShippingCity { get; set; }

        public List<OrderLine> OrderLines { get; set; }
        #endregion
    }
}
