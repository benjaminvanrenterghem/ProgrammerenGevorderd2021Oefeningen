using System;
using System.Collections.Generic;
//using System.Linq;

namespace ProcessorExample
{

    public class Calculator
    {
        #region Methods
        public double Average(List<int> values)
        {
            var sum = 0.0;
            foreach(var i in values)
            {
                sum += i;
            }
            return sum / values.Count;
            //return values.Average();[
        }

        public double Min(List<int> values)
        {
            var min = double.MaxValue;
            foreach(var i in values)
            {
                if (i < min)
                    min = i;
            }
            return min;
            //return values.Min();
        }

        public double Range(List<int> values)
        {
            return Max(values) - Min(values);
        }

        public double Max(List<int> values)
        {
            var max = double.MaxValue * -1;
            foreach (var i in values)
            {
                if (i > max)
                    max = i;
            }
            return max;
            //return values.Max();
        }
        #endregion
    }
}
