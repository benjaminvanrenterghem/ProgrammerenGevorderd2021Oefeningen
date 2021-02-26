using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ProcessorExample
{
    public class DataProcessor
    {
        #region Events
        public delegate double ProcessMethod(List<int> values);
        #endregion

        #region Properties
        private ProcessMethod _process;
        #endregion

        #region Methods
        private List<int> _values = new List<int>();
        private double _result;

        public DataProcessor(List<int> values)
        {
            foreach (var i in values)
                _values.Add(i);
        }

        public void SetMethod(ProcessMethod method)
        {
            _process = method;
        }

        public void ProcessValues()
        {
            _result = _process(_values);
        }

        public void PrintResult()
        {
            Console.WriteLine("result: {0}", _result);
        }

        public void PrintValues()
        {
            foreach (var v in _values)
                Console.WriteLine("value: {0}", v);
        }
        #endregion
    }
}
