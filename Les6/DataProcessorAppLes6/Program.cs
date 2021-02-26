using DataProcessorAppLes6;
using ProcessorExample;
using System.Collections.Generic;

namespace DataProcessorAppLes6
{

    class Program
    {
        static void Main(string[] args)
        {
            var n1 = new Number(10);
            var n2 = new Number(5000);

            n1.PrintNumber();
            n2.PrintMoney();

            var values = new List<int> { 1, 2, 3, 4, 5, 6 };
            var dataProcessor = new DataProcessor(values); // kent alleen de waarden, weet niet hoe te rekenen
            var calculator = new Calculator(); // weet als enige hoe te berekenen - kent geen waarden

            dataProcessor.SetMethod(calculator.Average);
            dataProcessor.SetPrint(Printer.PrintCsv);
            dataProcessor.PrintValues();
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();

            dataProcessor.SetMethod(calculator.Range);
            dataProcessor.SetPrint(Printer.PrintLines);
            dataProcessor.PrintValues();
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();

            /*
            dataProcessor.SetMethod(calculator.Max);
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();
            */
        }
    }
}
