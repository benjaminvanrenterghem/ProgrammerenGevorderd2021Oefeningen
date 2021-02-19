using ExceptionDemo;
using System;

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
                try
                {
                    quotient = deeltal / deler;
                    Console.WriteLine($"Resultaat: {quotient}");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Wie deelt door 0 is een snul!");
                }
                finally
                {
                    Console.WriteLine("Hier komen we altijd terecht - zonder en met exception");
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
        }
    }
}
