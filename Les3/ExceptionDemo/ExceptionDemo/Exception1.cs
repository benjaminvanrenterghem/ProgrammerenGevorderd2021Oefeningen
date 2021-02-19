using System;

namespace ExceptionDemo
{
    public class MyException: Exception
    {
        public override string ToString()
        {
            var extraInfo = "Mijn exception: ";
            return extraInfo + base.ToString();
        }
    }

    public class Exception1
    {
        public int X { get; set; }

        public void ExecuteRead()
        {
            try
            {
                string input = Console.ReadLine();
                int converted = Convert.ToInt32(input);
            }
            catch(FormatException ex)
            {
                System.Diagnostics.Debug.WriteLine("Je moet een integer ingeven: " + ex.ToString());
            }            
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Verkeerde invoer: " + e.Message);
            }                    
        }

        public void CallMe()
        {
            try
            {
                try
                {
                    //throw new NotImplementedException();
                    throw new Exception("Dit is mijn eigen uitzondering - ik moet dit nog implementeren");
                }
                catch (Exception e)
                {
                    throw new MyException();
                }
                System.Diagnostics.Debug.WriteLine("En we blijven doorgaan...");
            }
            catch(MyException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
    }
}
