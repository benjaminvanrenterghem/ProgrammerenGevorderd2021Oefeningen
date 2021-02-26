using ProcessorExample;
using System.Collections.Generic;

namespace DataProcessorAppLes6
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new List<int> { 1, 2, 3, 4, 5, 6 };
            var dataProcessor = new DataProcessor(values); // kent alleen de waarden, weet niet hoe te rekenen
            var calculator = new Calculator(); // weet als enige hoe te berekenen - kent geen waarden

            dataProcessor.SetMethod(calculator.Average);
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();

            dataProcessor.SetMethod(calculator.Min);
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();

            dataProcessor.SetMethod(calculator.Max);
            dataProcessor.ProcessValues();
            dataProcessor.PrintResult();
        }
    }
}
