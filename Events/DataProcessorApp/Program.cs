using System;
using System.Collections.Generic;
using ProcessorExample;

namespace DataProcessorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new List<int> { 1, 2, 3, 4, 5, 6 };
            var dataProcessor = new DataProcessor(values);
            var calculator = new Calculator();

            dataProcessor.SetMethod(calculator.Average);
            dataProcessor.PrintValues();
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();

            dataProcessor.SetMethod(calculator.Range);
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();
        }
    }
}
