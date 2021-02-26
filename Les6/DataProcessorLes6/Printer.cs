using System.Collections.Generic;
//using System.Linq;

namespace ProcessorExample
{
    public static class Printer
    {
        #region Methods
        public static void PrintCsv(List<int> values) // CSV: Comma Separated Value: 1,jef,Zoersel (soms ; als separator en soms ; als lijn separator; soms eerste regel kolomnamen
        {
            System.Diagnostics.Debug.WriteLine($"{string.Join(",", values)}");
        }

        public static void PrintLines(List<int> values)
        {
            System.Diagnostics.Debug.WriteLine($"{string.Join(System.Environment.NewLine, values)}"); // System.Environment.NewLine: juiste newline code voor Windows onder Windows, voor Linux onder Linus, ...
        }
        #endregion
    }
}
