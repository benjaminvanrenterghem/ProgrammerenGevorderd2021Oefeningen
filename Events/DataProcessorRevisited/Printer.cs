// HOGENT
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessorExample
{

    public static class Printer
    {
        #region Methods
        public static void PrintCsv(List<int> values)
        {
            Console.WriteLine($"{string.Join(",", values)}");
        }

        public static void PrintLines(List<int> values)
        {
            Console.WriteLine($"{string.Join(System.Environment.NewLine, values)}");
        }
        #endregion
    }
}
