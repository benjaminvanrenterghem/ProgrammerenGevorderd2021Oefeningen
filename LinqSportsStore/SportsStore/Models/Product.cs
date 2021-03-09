using System;

namespace SportsStore.Models
{
    public class Product
    {
        #region Properties
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            return (obj is Product p) ? p.ProductId == ProductId : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId);
        }
        #endregion
    }


}