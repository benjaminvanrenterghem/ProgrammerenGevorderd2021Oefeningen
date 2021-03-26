using System.Collections.Generic;

namespace SportsStoreAdoNetLes
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new DataSourceProvider();
            provider.Seed();

            IEnumerable<SportsStore.Domain.Category> categories = provider.Categories; // Elke List is een IEnumerable: erft over; IEnumerable kan enkel over elementen lopen van begin tot eind
            IEnumerable<SportsStore.Domain.Customer> customers = provider.Customers;
            IEnumerable<SportsStore.Domain.Product> products = provider.Products;

            foreach (var category in categories)
            {
                category.Save();
            }

            foreach (var customer in customers)
            {
                customer.Save();
            }
            foreach (var product in products)
            {
                product.Save();
            }
        }
    }
}
