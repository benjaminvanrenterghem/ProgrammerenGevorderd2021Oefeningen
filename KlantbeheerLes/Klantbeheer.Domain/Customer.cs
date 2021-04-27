using Klantbeheer.Domain.Exceptions.ModelExceptions;
using System;

namespace Klantbeheer.Domain
{

    public class Customer: DataObject
    {
        #region Properties
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int Discount { get; private set; }

        // lijst van Order
        #endregion

        #region Ctor
        public Customer(string name, string address)
        {
            SetName(name);
            SetAddress(address);           
        }

        public Customer(int id, string name, string address): this(name, address)
        {
            SetCustomerID(id);
        }
        #endregion

        #region Methods
        public void SetCustomerID(int id)
        {
            if (id <= 0) throw new CustomerException("CustomerID: ID can't be 0 or lower.");

            Id = id;
        }

        public void SetName(string name)
        {
            // Precondities
            if(string.IsNullOrEmpty(name)) throw new CustomerException("The name cannot be null");

            Name = name;
        }

        public void SetAddress(string address)
        {
            // Precondities
            if (string.IsNullOrEmpty(address)) throw new CustomerException("The address cannot be null");

            Address = address;
        }

        // Equals/ToHashCode/ToString

        public override bool Equals(object obj) => obj is Customer c && Name == c.Name && Address == c.Address;

        public override int GetHashCode() => HashCode.Combine(Name, Address);

        public override string ToString()
        {
            return $"#Customer# {Id} {Name} {Address} {Discount}";
        }
        #endregion
    }
}
