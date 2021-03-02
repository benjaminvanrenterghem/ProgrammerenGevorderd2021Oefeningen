using BusinessLayer;
using System;

namespace KlantOberKok
{
    class Program
    {
        static void Main(string[] args)
        {
            var klant1 = new Klant("Piet");
            var klant2 = new Klant("Jef");
            var bestellingsSysteem = new BestellingsSysteem(); // vb iPAD
            // Event:
            // ------
            var bel = new Bel();            
            var ober = new Ober("Jan")
            {
                BestellingsSysteem = bestellingsSysteem,
                // Event:
                // ------
                Bel = bel, // waarnaar hij moet luisteren - de consumer
            };
            // kok en ober weten niet van elkaar!
            var kok = new Kok("Marie")
            {
                BestellingsSysteem = bestellingsSysteem,
                // Event:
                // ------
                Bel = bel, // waarop hij moet duwen - de publisher, de oproeper
                // Interface: "dependency injection"
                // ----------
                // Bel = ober
            };           

            klant1.Bestel(ober, "Hoegaarden");
            klant2.Bestel(ober, "Koffie");

            Console.ReadKey();
        }
    }
}
