using System;
using System.Collections.Generic;

namespace SportsStore.Contracts
{
    public class Cart
    {
        #region Properties
        public int Id { get; set; }
        public DateTimeOffset ShoppingDate { get; set; }
        public List<CartLine> CartLines { get; set; }
        #endregion
    }
}
