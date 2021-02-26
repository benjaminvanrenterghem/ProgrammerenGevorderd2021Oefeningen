using ProcessorExample;
using System;
using System.Collections.Generic;

namespace DataProcessorRevisitedApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new List<int>() { 1, 2, 3, 4, 5 };
            var dataProcessor = new DataProcessor(values);
            var calculator = new Calculator();

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

            Console.WriteLine("demo".SurroundWith("***"));

        }      
    }
}
