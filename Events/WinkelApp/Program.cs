using System;
using WinkelManagement;

namespace WinkelApp
{
    class Program
    {
        static void Main()
        {
            Winkel winkel = new Winkel();
            Sales sales = new Sales();
            Stockbeheer stockBeheer = new Stockbeheer();
            Groothandelaar groothandelaar = new Groothandelaar();
            winkel.ProductVerkocht += sales.OnProductVerkocht;  // Event subscription
            winkel.ProductVerkocht += stockBeheer.OnProductVerkocht;    // Event subscription
            stockBeheer.StockOnderMinimum += groothandelaar.OnStockOnderMinimum;    // Event subscription
            groothandelaar.BestellingGeplaatst += stockBeheer.OnBestellingGeplaatst;    // Event subscription
            stockBeheer.ShowStock();    // Initiële stock
            groothandelaar.ShowLaatsteBestelling(); // Initiële laatste bestelling
            groothandelaar.ShowAlleBestellingen(); // Initieel bestellingenoverzicht

            // Verkoop van winkelproducten
            Console.WriteLine("Verkoop van producten...");
            winkel.VerkoopProduct(new Bestelling(ProductType.Dubbel, 3.99, 35, "Dorpstraat 5, Lievegem"));
            winkel.VerkoopProduct(new Bestelling(ProductType.Kriek, 2.99, 25, "Dorpstraat 5, Lievegem"));
            winkel.VerkoopProduct(new Bestelling(ProductType.Dubbel, 3.99, 35, "Kerkstraat 155, Zele"));
            winkel.VerkoopProduct(new Bestelling(ProductType.Kriek, 2.99, 55, "Dorpstraat 5, Lievegem"));


            stockBeheer.ShowStock();    // Stock na verkoop
            sales.ShowRapport();    // Salesrapport na verkoop
            groothandelaar.ShowLaatsteBestelling(); // Laatste bestelling na verkoop
            groothandelaar.ShowAlleBestellingen(); // Bestellingenoverzicht na verkoop
            stockBeheer.ShowStock();    // Stock na stockaanvulling
            winkel.VerkoopProduct(new Bestelling(ProductType.Dubbel, 3.99, 30, "Kerkstraat 155, Zele"));
            stockBeheer.ShowStock();    // Stock na stockaanvulling
            groothandelaar.ShowAlleBestellingen(); // Bestellingenoverzicht na verkoop
        }
    }
}
