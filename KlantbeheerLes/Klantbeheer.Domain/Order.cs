using Klantbeheer.Domain.Exceptions.ModelExceptions;
using System;
using System.Collections.Generic;

namespace Klantbeheer.Domain
{
    public class Order: DataObject
    {
        #region Properties
        private readonly Dictionary<Product, int> _products = new();
        public bool IsPaid { get; set; } = false;
        public double PricePaid { get; set; }
        public DateTime OrderTime { get; private set; }
        public Customer Customer { get; private set; }
        #endregion

        #region Ctor
        public Order(int id, DateTime datetime)
        {
            SetOrderID(id);
            SetDatetime(datetime);
        }

        public Order(int id, DateTime datetime, Customer customer) : this(id,datetime)
        {
            SetCustomer(customer);
        }

        public Order(int id, DateTime datetime, Customer customer, Dictionary<Product,int> products) : this(id,datetime,customer)
        {
            if (products == null) throw new OrderException("Products in order: products can't be null.");
            if(products.Count == 0) throw new OrderException("Products in order: list of products is empty.");

            _products = products;
        }
        #endregion

        #region METHODS
        public void AddProduct(Product product, int amount)
        {
            if (amount > 0 && product != null)
            {
                if (_products.ContainsKey(product)) _products[product] += amount;
                else _products.Add(product, amount);
            }
            else if (amount <= 0 && product != null) throw new OrderException("The amount of the product can't be 0 or lower");
            else if (amount > 0 && product == null) throw new OrderException("Product can't be null");
            else throw new OrderException("the amount of the product can't be 0 or lower and product can't be null");
        }

        public void RemoveProduct(Product product, int amount)
        {
            if (amount <= 0 || amount > _products[product]) throw new OrderException("RemoveProduct: invalid amount.");
            else
            {
                if (!_products.ContainsKey(product)) throw new OrderException("RemoveProduct: product not available.");
                else
                {
                    if (_products[product] < amount) throw new OrderException("RemoveProduct: not enough products to remove");
                    else if (_products[product] == amount) _products.Remove(product);
                    else _products[product] -= amount;
                }
            }
        }

        public void SetIsPaid(bool isPayed = true)
        {
            IsPaid = isPayed;
        }

        public void SetPricePayed()
        {
            if (!IsPaid)
            {
                return;
            }
            double priceProducts = 0;

            foreach (var p in _products)
            {
                priceProducts += p.Key.Price * p.Value;
            }

            if (Customer == null)
            {
                PricePaid = priceProducts;
            }
            else
            {
                int discount = Customer.Discount;

                if (discount == 0) PricePaid = priceProducts;
                else PricePaid = priceProducts - (priceProducts * discount / 100);
            }
        }

        public void SetOrderID(int id)
        {
            if (id < 0) throw new OrderException("Order - SetOrderID: invalid ID");
            Id = id;
        }
        public void SetDatetime(DateTime dateTime)
        {
            //if (dateTime == null) throw new OrderException("OrderTime: Time can't be null.");
            //else 
            OrderTime = dateTime;
        }

        public void SetCustomer(Customer customer)
        {
            if (customer == null) throw new OrderException("Customer of order: Customer can't be null.");
            if (Customer == customer) throw new OrderException("Customer of order: not a new Customer.");

            //if (Customer != null)
            //{
            //    if (Customer.HasOrder(this)) Customer.RemoveOrder(this);
            //}
            //if (!customer.HasOrder(this)) customer.AddOrder(this);
            Customer = customer;
        }

        public void RemoveCustomer()
        {
            Customer = null;
        }

        public IReadOnlyDictionary<Product,int> GetProducts()
        {
            return _products;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order && Id == order.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"#Order# {Id} {IsPaid} {PricePaid} {Customer} {OrderTime} {_products.Count}";
        }

        public void Show()
        {
            Console.WriteLine(this);
            foreach(KeyValuePair<Product,int> product in _products)
            {
                Console.WriteLine($"------Product: {product.Key} --> {product.Value}");
            }
        }
        #endregion
    }
}
