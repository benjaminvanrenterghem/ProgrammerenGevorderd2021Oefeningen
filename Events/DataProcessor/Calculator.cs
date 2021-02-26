using System.Collections.Generic;
using System.Linq;

namespace ProcessorExample
{
    public class Calculator
    {
        #region Methods
        public double Average(List<int> values)
        {
            /*
            var sum = 0.0;
            foreach(var i in values) { sum += i; }
            return sum / values.Count(); // LINQ!
            */
            return values.Average(); // LINQ
        }

        public double Min(List<int> values)
        {
            return values.Min(); // LINQ
        }

        public double Max(List<int> values)
        {
            return values.Max(); // LINQ
        }

        public double Range(List<int> values)
        {
            return Max(values) - Min(values);
        }
        #endregion
    }
}
