using ExceptionDemo;
using System;
using System.Collections.Generic;

namespace ExceptionDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int deler, deeltal, quotient;
            Console.Write("Deeltal: ");
            deeltal = Convert.ToInt32(Console.ReadLine());
            Console.Write("Deler: ");
            deler = Convert.ToInt32(Console.ReadLine());
            //if(deler == 0)
            //{
            //    Console.WriteLine("Wie deelt door 0 is een snul!");
            //}
            //else
            {
                //MyDabaseConnection connection = new MyDatabaseConnection(); // tcp connectie naar server met lokale resources, vb. geheugen
                try
                {
                    quotient = deeltal / deler;
                    Console.WriteLine($"Resultaat: {quotient}");
                }
                catch /*(Exception e)*/
                {
                    Console.WriteLine("Wie deelt door 0 is een snul!");
                    throw;
                }
                finally
                {
                    Console.WriteLine("Hier komen we ALTIJD terecht - zonder en met het optreden van een exception!!");
                    //connection.Close();
                }
            }

            {
                int i = 5; // POD
                int j = i + 1;

                Exception1 myObject = null; // = new Exception1();
                try
                {
                    myObject.X = 5;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Ik heb een fout voor: " + e.Message);
                    //throw; // alsof er geen catch stond!
                    // throw e; // is ook goed, maar niet aangeraden als je enkel de exception terug doorgeeft
                }
                System.Diagnostics.Debug.WriteLine("We gaan toch door met onze applicatie dankzij de catch");

                myObject = new Exception1();
                myObject.ExecuteRead();
                myObject.CallMe();
            }

            {
                var superman1 = new SuperMan();
                var spiderman1 = new SpiderMan();
                List<Man> mannen = new List<Man> { superman1, spiderman1 };
                foreach(var m in mannen)
                {
                    System.Diagnostics.Debug.WriteLine(m.Power);
                }
                List<ISuperHeld> superhelden = new List<ISuperHeld> { superman1, spiderman1 };
                foreach(var s in superhelden)
                {
                    s.SchietLasers();
                }
            }
        }
    }
}
