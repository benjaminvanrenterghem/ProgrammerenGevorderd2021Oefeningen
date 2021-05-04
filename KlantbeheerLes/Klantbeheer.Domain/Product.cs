using Klantbeheer.Domain.Exceptions.ModelExceptions;
using System;

namespace Klantbeheer.Domain
{
    public class Product: DataObject
    {
        
        #region Properties
        public string Name { get; private set; }
        public double Price { get; private set; }
        public bool IsActive { get; set; }
        #endregion

        #region Ctors
        public Product(string name) => SetName(name);
        public Product(string name, double price) : this(name) => SetPrice(price);
        public Product(int productId, string name, double price) : this(name, price) => SetProductID(productId);
        #endregion

        #region Methods
        public void SetName(string name)
        {
            if (name == null) throw new ProductException("Productname: name can't be null"); 
            if(name.Trim().Length < 1) throw new ProductException("Productname: name is invalid");
            Name = name;
        }

        public void SetPrice(double price)
        {
            if (price < 0) throw new ProductException("Product Price: price can't be lower than 0");

            Price = price;
        }
        public void SetProductID(int productId)
        {
            if (productId <= 0) throw new ProductException("ProductID: ID is invalid.");

            Id = productId;
        }
        public override string ToString()
        {
            return $"#Product# {Id}, {Name}, {Price}";
        }
        public override bool Equals(object obj)
        {
            return obj is Product p && Name == p.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
