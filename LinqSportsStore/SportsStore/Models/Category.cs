using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Category
    {
        #region Properties
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Constructor and Methods
        public Category() => Products = new List<Product>();

        public void AddProduct(string naam, decimal price, string description)
        {
            if (Products.FirstOrDefault(p => p.Name == naam) == null)
                Products.Add(new Product() { Description = description, Name = naam, Price = price });
        }

        public Product FindProduct(string naam) => Products.FirstOrDefault(p => p.Name == naam);
        #endregion
    }
}
