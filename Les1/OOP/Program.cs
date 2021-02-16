using System;
using System.Collections.Generic;
using KleineOefeningen;
using Ziekenhuis;
//using BalSpel1;
using Vaccination;

namespace BierWinkel
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Testcode Student
            Student student1 = new Student { Klas = Klassen.EA1, Naam = "Jef", Leeftijd = 17, PuntenCommunicatie = 17, PuntenProgrammingPrinciples = 16, PuntenWebTech = 9 };
            student1.GeefOverzicht();
            #endregion

            #region Testcode BierWinkel
            {
                var b = new BierWinkel.Bier(1.05, "palm", "Amber", "palm", 25, 5.2, 6);
                var inventaris = new BierWinkel.Inventaris();
                inventaris.VoegBierToe(1.05, "palm", "amber", "palm", 25, 5.2, 6);
                inventaris.VoegBierToe(1.25, "rodenbach classic", "bruin", "palm", 25, 5.2, 6);
                inventaris.VoegBierToe(1.6, "leffe bruin", "bruin", "leffe", 33, 6.2, 6);
                inventaris.VoegBierToe(1.8, "duvel", "blond", "duvel moortgat", 33, 8.5, 6);
                var x = inventaris.SelecteerBier("palm");
                Console.WriteLine($"Bier: {x}");
                var y = inventaris.ZoekBier(b);
                Console.WriteLine($"Bier: {y}");
            }
            #endregion

            #region Dier
            var aDier = new Dier();
            var aPaard = new Paard();
            aDier.Eet();
            aPaard.Eet();
            aPaard.KanHinnikken = false;
            //aDier.KanHinnikken = false; //!!! zal niet werken!
            #endregion

            #region Soldaat
            var mijnDokter = new Medic(true);
            #endregion

            #region Vliegtuig
            var vliegtuigen = new List<Vliegtuig>();

            var f1 = new Vliegtuig();
            var spaceX1 = new SupersonischVliegtuig();

            vliegtuigen.Add(f1);
            vliegtuigen.Add(spaceX1);

            var spaceX1AlsVliegtuig = (Vliegtuig)spaceX1;
            spaceX1.Start();
            spaceX1AlsVliegtuig.Start();
            spaceX1.Vlieg();
            spaceX1AlsVliegtuig.Vlieg();

            foreach (var vliegtuig in vliegtuigen)
            {
                vliegtuig.Start();
                vliegtuig.Vlieg();
            }
            #endregion

            #region Animals

            List<Animal> animals = new List<Animal>
            {
                new Rabbit(),
                new Snake(),
                new Animal()
            };

            foreach (var a in animals)
            {
                a.ToonInfo();
            }
            #endregion

            #region Ziekenhuis
            Patient JosFromUSA = new Patient() { Naam = "American Jos", UrenInZiekenhuis = 10 };
            VerzekerdePatient JosFromBelgium = new VerzekerdePatient() { Naam = "Belgische Jos", UrenInZiekenhuis = 10 };
            JosFromUSA.ToonInfo();
            JosFromBelgium.ToonInfo();

            // Polymorfisme:
            List<Patient> allePatienten = new List<Patient>() // lijst van base class objecten
            {
                new Patient() { Naam = "American Jos", UrenInZiekenhuis = 10 },
                new VerzekerdePatient() { Naam = "Belgische Jos", UrenInZiekenhuis = 10 },
            };

            foreach (var patient in allePatienten)
            {
                patient.ToonInfo();
            }
            #endregion

            /*
            #region Balspel
            Console.CursorVisible = false;
            Console.WindowHeight = 20;
            Console.WindowWidth = 30;
            Ball b1 = new Ball(4, 4, 1, 0);
            PlayerBall player = new PlayerBall(10, 10, 0, 0);
            while (true)
            {

                Console.Clear();

                //Ball
                b1.Update();
                b1.Draw();

                //SpelerBall
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    player.ChangeVelocity(key);
                }

                player.Update();
                player.Draw();

                //Check collisions
                if (Ball.CheckHit(b1, player))
                {
                    Console.Clear();
                    Console.WriteLine("Gewonnen!");
                    Console.ReadLine();
                }
                System.Threading.Thread.Sleep(100);
            }
            #endregion
            */

            #region Vaccin
            var v = new Virus();
            var vaccins = new List<Vaccin>();
            for (int i = 0; i < 5; i++)
            {
                vaccins.Add(new Vaccin(i.ToString()));
            }

            Vaccin theCure = null; // we kennen nog geen oplossing
            while (v.DoomCountDown > 0 && theCure == null)
            {
                foreach (Vaccin vaccin in vaccins)
                {
                    if (v.TryVaccin(vaccin) == true)
                    {
                        theCure = vaccin;
                        break;
                    }
                }
            }

            if (theCure != null)
            {
                //Fase 2
                theCure.ToonInfo();
                VaccinatieCentrum.BewaarVaccin(theCure.Oplossing); // static method: geldt dus voor alle objecten deze class, dus voor alle vaccinatiecentra

                var centra = new List<VaccinatieCentrum>();
                for (int i = 0; i < 5; i++)
                {
                    centra.Add(new VaccinatieCentrum());
                }

                var containerVaccins = new List<Vaccin>();
                foreach (var centrum in centra)
                {
                    for (int i = 0; i < 7; i++)
                    {

                        containerVaccins.Add(centrum.GeefVaccin());
                    }
                }


                for (int i = 0; i < containerVaccins.Count; i++)
                {
                    Console.Write(i + " ");
                    containerVaccins[i].ToonInfo();
                }

            }
            else
            {
                Console.WriteLine("Gedaan");
            }
            #endregion
        }
    }
}
