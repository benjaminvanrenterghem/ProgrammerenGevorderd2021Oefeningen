using ExceptionDemo;
using System;

namespace ExceptionDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 5; // POD
            int j = i + 1;

            Exception1 myObject = null; // = new Exception1();
            try
            {
                myObject.X = 5;
            }
            catch(Exception e)
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
