using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace SportsStoreAdoNetLes
{
    public class DataSourceProvider
    {
        #region Fields
        string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        private List<SportsStore.Domain.Category> _categories;
        private List<SportsStore.Domain.Customer> _customers;

        public List<SportsStore.Domain.Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
            }
        }
        public List<SportsStore.Domain.Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
            }
        }
        public List<SportsStore.Domain.Product> Products
        {
            get
            {
                return _categories.SelectMany(c => c.Products).ToList();
            }
        }

        public DataSourceProvider()
        {
        }

        internal void Seed()
        {
            if (_categories != null)
                return;

            var watersports = new SportsStore.Domain.Category() { Name = "WaterSports" };
            var soccer = new SportsStore.Domain.Category() { Name = "Soccer" };

            // We need to save to generate the id the first time around
            watersports.Save();
            soccer.Save();

            _categories = new List<SportsStore.Domain.Category> { watersports, soccer };

            soccer.AddProduct("Football", 25, "WK colors");
            soccer.AddProduct("Corner flags", 34.95M, "Give your playing field that professional touch");
            soccer.AddProduct("Running shoes", 95, "Protective and fashionable");
            watersports.AddProduct("Surf board", 275, "A boat for one person");
            watersports.AddProduct("Kayak", 170, "High quality");
            watersports.AddProduct("Lifejacket", 49.99M, "Protective and fashionable");

            /*
            var fileProcessor = new FileProcessor(@"c:\tmp", "extract", "adresInfo.zip"); // root directory, subdirectory voor extractie, bestandsnaam zip file
            fileProcessor.UnZip();
            fileProcessor.ReadFile("adresInfo.txt");

            var citiesToStore = new List<SportsStore.Domain.City>();
            foreach (var city in fileProcessor.Cities)
            {
                var myCity = new SportsStore.Domain..City(new SportsStore.Contracts.City { Name = city });
                // Convert to switch:
                if (myCity.Name == "Gent") myCity.PostalCode = "9000";
                else if (myCity.Name == "Antwerpen") myCity.PostalCode = "2000";
                citiesToStore.Add(myCity);
            }
            */
            var gent = new SportsStore.Domain.City { Name = "Gent", PostalCode = "9000" };
            var antwerpen = new SportsStore.Domain.City { Name = "Antwerpen", PostalCode = "3000" };
            SportsStore.Domain.City[] citiesToStore = { gent, antwerpen };

            // We need to save to generate the id the first time around
            gent.Save();
            antwerpen.Save();
            foreach (var c in citiesToStore) c.Save();

            var r = new Random();
            char[] letters = { 'a', 'e', 'i', 'o', 'u', 'b', 'd', 'f', 'g', 'h' };
            _customers = new List<SportsStore.Domain.Customer>();
            for (int i = 1; i <= 10; i++)
            {
                var klant = new SportsStore.Domain.Customer
                {
                    CustomerName = "student" + i,
                    Name = "Student" + i,
                    FirstName = "Jan" + letters[r.Next(10)],
                    Street = "Nieuwstraat " + i,
                    City = citiesToStore[r.Next(2)]
                };
                _customers.Add(klant);
                var cart = new SportsStore.Domain.Cart();
                cart.Save();
                cart.AddLine(soccer.FindProduct("Football"), 1);
                cart.AddLine(soccer.FindProduct("Corner flags"), 2);
                klant.Save();
                klant.PlaceOrder(cart, DateTime.Today, false, klant.Street, klant.City);
            }
        }

        /*
        1. Database aanmaken (SportsStore.sql)
        2. Dit programma laten lopen om data in te vullen
        3. Data nakijken met SSMS
        ------
        4. 4 queries schrijven in SSMS
        6. Laten lopen en vergelijken met LINQ
        */

        // Hoeveel customers
        internal int GetCustomerCount()
        {
            // TODO(lvet):
            return 0;
        }

        // Hoeveel categorieen
        internal int GetCategoryCount()
        {
            // TODO(lvet):
            return 0;
        }

        // Gemiddelde prijs alle producten
        internal double GetAveragePrice()
        {
            // TODO(lvet):
            return 0.0;
        }

        // Gemiddelde prijs producten per category
        internal DataTable GetAvgPrices()
        {
            // TODO(lvet):
            return null;
        }
    }
}