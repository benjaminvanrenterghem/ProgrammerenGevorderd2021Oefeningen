using SportsStore.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SportsStore.Data
{
    public static class DataSourceProvider
    {
        #region Properties
        private static List<Category> _categories;
        private static List<Customer> _customers;

        public static IEnumerable<Category> Categories
        {
            get
            {
                Seed();
                return _categories;
            }
        }
        public static IEnumerable<Customer> Customers
        {
            get
            {
                Seed();
                return _customers;
            }
        }
        public static IEnumerable<Product> Products
        {
            get
            {
                Seed();
                return _categories.SelectMany(c => c.Products);
            }
        }
        #endregion

        #region Methods
        private static void Seed()
        {
            if (_categories != null)
                return;
            Category watersports = new Category() { Name = "WaterSports" };
            Category soccer = new Category() { Name = "Soccer" };
            _categories = new List<Category> { watersports, soccer };

            soccer.AddProduct("Football", 25, "WK colors");
            soccer.AddProduct("Corner flags", 34.95M, "Give your playing field that professional touch");
            soccer.AddProduct("Running shoes", 95, "Protective and fashionable");
            watersports.AddProduct("Surf board", 275, "A boat for one person");
            watersports.AddProduct("Kayak", 170, "High quality");
            watersports.AddProduct("Lifejacket", 49.99M, "Protective and fashionable");

            City gent = new City { Name = "Gent", Postalcode = "9000" };
            City antwerpen = new City { Name = "Antwerpen", Postalcode = "3000" };
            City[] cities = { gent, antwerpen };

            Random r = new Random();
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            _customers = new List<Customer>();
            for (int i = 1; i < 10; i++)
            {
                int j = i / 2;
                Customer klant = new Customer
                {
                    CustomerName = "student" + j,
                    Name = "Student" + j,
                    FirstName = "Jan" + vowels[r.Next(5)],
                    Street = "Nieuwstraat 100",
                    City = cities[r.Next(2)]
                };
                _customers.Add(klant);
                if (i <= 5)
                {
                    Cart cart = new Cart();
                    cart.AddLine(soccer.FindProduct("Football"), 1);
                    cart.AddLine(soccer.FindProduct("Corner flags"), 2);
                    klant.PlaceOrder(cart, DateTime.Today, false, klant.Street, klant.City);
                }
            }
        }
        #endregion
    }
}