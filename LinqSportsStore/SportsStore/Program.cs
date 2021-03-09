using SportsStore.Data;
using SportsStore.Models;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq; // LINQ = extension methods!!! Dit betekent: using toevoegen of je ziet niet dat deze mogelijk zijn met Intellisense

namespace SportsStore
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Category> categories = DataSourceProvider.Categories.ToList();
            List<Customer> customers = DataSourceProvider.Customers.ToList();
            List<Product> products = DataSourceProvider.Products.ToList();

            #region 1. Toon de gemiddelde prijs van de producten
            var gemiddelde = products.Average(p => p.Price); //  products. LINQ invullen: Average
            Console.WriteLine($"De gemiddelde prijs van de producten is { gemiddelde:0.00}");
            #endregion

            #region 2. Toon hoeveel categorieen en hoeveel customers er zijn
            Console.WriteLine($"Er zijn {categories.Count()} categorieen.");
            Console.WriteLine($"Er zijn {customers.Count()} customers.");
            #endregion

            #region 3. Hoeveel karakters telt de langste productnaam?
            // Lengte laatste productnaam
            // var langsteNaam = products.OrderByDescending(s => s.Name).FirstOrDefault(); // geeft null terug wanneer er geen product is
            var langsteNaamLengte = products.Max(p => p.Name.Length);
            Console.WriteLine($"De langste productnaam is {langsteNaamLengte} karakters lang.");
            #endregion

            #region 4. Toon de naam van het product met de langste productnaam
            var productnaam = products.OrderByDescending(p => p.Name.Length).FirstOrDefault()?.Name;
            Console.WriteLine($"De langste productnaam is {productnaam}.");
            #endregion

            #region 5. Toon alle customers gesorteerd op naam, en vervolgens dalend op voornaam
            var customersSorted = customers.OrderBy(c => c.Name).ThenByDescending(c => c.FirstName);
            PrintCustomers("Klanten gesorteerd op naam, en dan dalend op voornaam:", customersSorted);
            #endregion

            #region 6. Toon alle producten die meer dan 92.5 dollar kosten, dalend op prijs
            // query syntax:
            // var expensiveProducts = from p in products where p.Price > 92.5M orderby p.Price descending select p;
            // method syntax:
            // var expensiveProducts = products.Where(p => p.Price > 92.5M).OrderByDescending(p => p.Price);
            var expensiveProducts = products.Where(p => p.Price > (decimal)92.5).OrderByDescending(p => p.Price);
            // M modifier, staat voor M(oney)
            // double: modifier D/d daardoor al benomen
            // decimal: modifier M/m omdat D/d al benomen was; decimals worden veel gebruikt als comma-getallen voor bankieren - geen afrondingsfouten, maar afgekapte komma-getallen
            PrintProducts("Producten die meer dan 92.5 dollar kosten", expensiveProducts);
            #endregion

            #region 7. Toon de categorieen die meer dan twee producten bevatten
            var myCategories = categories.Where(c => c.Products.Count > 2);
            PrintCategories("Categorieën met meer dan twee producten", myCategories);
            #endregion

            #region 8. Maak een lijst van strings die alle productnamen bevat
            // Projectie: dus een Select
            var productNamen = products.Select(p => p.Name);
            PrintStrings("Namen van producten", productNamen);
            #endregion

            #region 9. Maak een lijst van namen van steden waar customers wonen (zonder dubbels) 
            var steden = customers.Select(c => c.City.Name).Distinct();
            PrintStrings("Namen van steden waar klanten wonen", steden);
            #endregion

            #region 10. Maak een lijst van ProductViewModels (vorm elk product om tot een productViewModel; factor: 0.9 voor PriceEuro)
            // Op weg naar WPF en modelering in lagen...

            var pvm = products.Select( p => new ProductViewModel { Name = p.Name, Price = p.Price /*, PriceEuro = p.Price * 0.9M*/ } );
            Console.WriteLine("Lijst van ProductViewModels");
            foreach (var p in pvm)
            {
                Console.WriteLine($"{p.Name} kost { p.Price:0.00} dollar, dat is {p.PriceEuro:0.00} euro");
            }
            Console.WriteLine();
            #endregion

            #region 11. Maak gebruik van een anoniem type 
            // maak een lijst die de naam, de voornaam
            // en de naam van de stad van elke customer bevat
            var customerDetails = customers.Select(c => new { /*Naam = */c.Name, c.FirstName, CityName = c.City.Name });
            Console.WriteLine("Details van customers");
            foreach (var c in customerDetails)
            {
                Console.WriteLine($"{0} {0} woont in {0}");
            }
            #endregion

            #region 12. Pas vorige query aan 
            // zodat het anoniem type nu ook een boolse property bevat
            // die aangeeft of de customer reeds orders heeft
            var customerDetails2 = customers.Select(c => new { c.Name, c.FirstName, CityName = c.City.Name, HasOrders = c.Orders.Count > 0 }); // C# leidt zelf af dat nieuw veld HasOrders een bool is
            Console.WriteLine("Details van customers");
            foreach (var c in customerDetails2)
            {
                Console.WriteLine($"{0} {0} woont in {0} en heeft {0} bestellingen");
            }
            #endregion

            #region 13. Geef de namen van de categorieën met enkel producten die de letter 'o' in de naam hebben (voor alle producten in de categorie moet gelden dat er een o in de naam voorkomt)

            // All: voor alle producten moet gelden dat er een o in de naam voorkomt
            // Any: als er minstens een product met een o in de naam
            // Wordt niet zoveel gebruikt in de praktijk
            var oCategories = categories.Where(c => c.Products.All(p => p.Name.Contains("o"))).Select(c => c.Name);
            PrintStrings("Categorieën waarbij alle producten de letter 'o' bevatten", oCategories);
            #endregion

            #region 14. Geef het eerste product dat de letter 'f' bevat, vertrek van de lijst van producten gesorteerd op naam
            // First(), Last(), FirstOrDefault(), ... worden wel veel gebruikt!
            var productList = products.OrderBy(p => p.Name);
            Product myProductF = products.OrderBy(p => p.Name).FirstOrDefault(p => p.Name.Contains("f"));
            Product myProductF2 = products.OrderBy(p => p.Name).Where(p => p.Name.Contains("f")).FirstOrDefault();
            PrintProduct("Eerste product met letter f", myProductF);
            #endregion

            #region 15. Maak een lijst van customers die reeds een product met de naam Football hebben besteld
            IEnumerable<Customer> customersWithFootball = customers.Where(c => c.Orders.Any(o => o.OrderLines.Any(ol => ol.Product.Name == "Football")));
            PrintCustomers("Klanten die reeds Football bestelden:", customersWithFootball);
            #endregion

            #region 16. Toon de voornaam van de customer met naam Student1.
            // Geef een gepaste melding indien
            //  - er geen customers zijn met die naam, 
            //  - of indien er meerdere customers zijn met die naam  
            // Test je code ook met Student0 en Student9
            var naam = "Student9";
            try
            {
                // hier invullen
                var customer = customers.SingleOrDefault(customers => customers.Name.Equals(naam));
                if (customer == null)
                    Console.WriteLine($"Er zijn geen customers met de naam {naam} gevonden");
            }
            catch (Exception e)
            {
                // hier invullen
                Console.WriteLine($"Er zijn meer customers gevonden met de naam {naam}: " + e.Message);
            }
            #endregion
        }

        #region 17. Pas de klasse Cart aan en maak zoveel mogelijk gebruik van expression bodied members
        //Pas de klasse Cart aan
        #endregion

        #region Print helpmethodes
        private static void PrintCustomers(string message, IEnumerable<Customer> customers)
        {
            Console.WriteLine(message);
            foreach (Customer c in customers)
                Console.WriteLine($"{ c.Name} {c.FirstName}");
            Console.WriteLine();
        }

        private static void PrintProducts(string message, IEnumerable<Product> products)
        {
            Console.WriteLine(message);
            foreach (Product p in products)
                Console.WriteLine($"{ p.Name}, prijs: { p.Price}");
            Console.WriteLine();
        }

        private static void PrintCategories(string message, IEnumerable<Category> categories)
        {
            Console.WriteLine(message);
            foreach (Category c in categories)
                Console.WriteLine(c.Name);
            Console.WriteLine();
        }

        private static void PrintStrings(string message, IEnumerable<string> strings)
        {
            Console.WriteLine(message);
            foreach (string s in strings)
                Console.WriteLine(s);
            Console.WriteLine();
        }

        private static void PrintProduct(string message, Product product)
        {
            Console.WriteLine(message);
            if (product == null)
                Console.WriteLine("Product is null");
            else
                Console.WriteLine($"{ product.Name}, prijs: {product.Price}");

            Console.WriteLine();
        }
        #endregion
    }
}

