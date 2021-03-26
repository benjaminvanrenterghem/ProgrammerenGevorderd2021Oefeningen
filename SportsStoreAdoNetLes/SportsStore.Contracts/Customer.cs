using System.Collections.Generic;

namespace SportsStore.Contracts
{
    public class Customer
    {
        #region Properties
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }

        public City City { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
        #endregion
    }
}
