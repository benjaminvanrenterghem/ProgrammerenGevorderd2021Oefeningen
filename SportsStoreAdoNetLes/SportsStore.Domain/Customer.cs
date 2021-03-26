using SportsStore.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class Customer: IPersist
    {
        #region Fields
        private Contracts.Customer _data;
        private bool _isDirty;
        #endregion

        #region Properties
        public Contracts.Customer Data { get { return _data; } }
        public int Id { get { return _data.Id; } set { if (_data.Id != value) { _data.Id = value; _isDirty = true; } } }
        public string CustomerName { get { return _data.CustomerName; } set { if (_data.CustomerName != value) { _data.CustomerName = value; _isDirty = true; } } }
        public string Name { get { return _data.Name; } set { if (_data.Name != value) { _data.Name = value; _isDirty = true; } } }
        public string FirstName { get { return _data.FirstName; } set { if (_data.FirstName != value) { _data.FirstName = value; _isDirty = true; } } }
        public string Street { get { return _data.Street; } set { if (_data.Street != value) { _data.Street = value; _isDirty = true; } } }

        private City _city;
        public City City { get { return _city; } set { if (_city == null || _city.Id != value.Id) { _city = value; _data.City = value.Data; _isDirty = true; } } }

        public List<Order> Orders { get; set; } = new List<Order>();
        #endregion



        #region Ctor
        public Customer()
        {
            _isDirty = true;
            _data = new Contracts.Customer();
        }
        public Customer(Contracts.Customer customer)
        {
            _data = customer;
            if (DAL.Customer.Exists(customer))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Methods       
        public void PlaceOrder(Cart cart, DateTimeOffset deliveryDate, bool giftWrapping, string shippingStreet, City shippingCity)
        {
            var newOrder = new Contracts.Order 
            { 
                CustomerId = Id, OrderDate = DateTimeOffset.Now, DeliveryDate = deliveryDate, GiftWrapping = giftWrapping, ShippingStreet = shippingStreet, ShippingCity = shippingCity.Data 
            };
            Orders.Add(new Order(newOrder));
        }

        #region IPersist
        public void Load()
        {
            if (_data.Id == 0) return;
            DAL.Customer.Select(_data);
        } 
        
        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.Customer.Exists(_data))
                {
                    DAL.Customer.Insert(_data);
                }
                else
                {
                    DAL.Customer.Update(_data);
                }
                foreach(var order in Orders)
                {
                    order.Save();
                }
            }
        }
        #endregion
        #endregion
    }
}
