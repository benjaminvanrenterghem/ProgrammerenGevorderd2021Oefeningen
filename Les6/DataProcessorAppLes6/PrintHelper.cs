namespace DataProcessorAppLes6
{
    public class PrintHelper
    {
        // Vanuit het oogpunt van de PUBLISHER!

        public delegate void BeforePrint(string message); // delegate is een keywoord

        public event BeforePrint BeforePrintEvent; // event is een keyword in C#: ingebakken in de taal

        public void PrintNumber(int num)
        {
            if (BeforePrintEvent != null) // event altijd testen: indien nog null, dan is er nog geen andere partij die zich geabonneerd heeft op deze event; anders: exception!
                BeforePrintEvent/*.Invoke*/("PrintNumber"); // hier zeg je: "event treedt op", dit wil zeggen dat wie erop ingeschreven is, een actie begint uit te voeren
            System.Diagnostics.Debug.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintDecimal(int dec)
        {
            if (BeforePrintEvent != null)
                BeforePrintEvent("PrintDecimal");
            System.Diagnostics.Debug.WriteLine("Decimal: {0:G}", dec);
        }

        public void PrintMoney(int money)
        {
            if (BeforePrintEvent != null)
                BeforePrintEvent("PrintMoney");
            System.Diagnostics.Debug.WriteLine("Money: {0:C}", money);
        }

        public void PrintTemperature(int num)
        {
            BeforePrintEvent?.Invoke("PrintTemperature"); // verkorte notatie van een null test, waarbij plots blijkt waarom Microsoft Invoke() aanbiedt
            System.Diagnostics.Debug.WriteLine("Temperature: {0,4:N1} F", num);
        }

        public void PrintHexadecimal(int dec)
        {
            BeforePrintEvent?.Invoke("PrintHexadecimal");
            System.Diagnostics.Debug.WriteLine("Hexadecimal: {0:X}", dec);
        }
    }
}
