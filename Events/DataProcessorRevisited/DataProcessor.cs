using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ProcessorExample
{
    public class DataProcessor
    {
        #region Events
        public Func<List<int>, double> _process;
        private Action<List<int>> _printOut;
        #endregion

        #region Properties
        private List<int> _values = new List<int>();
        private double _result;
        #endregion

        #region Ctor
        public DataProcessor(List<int> values)
        {
            foreach (var i in values)
                _values.Add(i);
        }
        #endregion

        #region Methods
        public void SetMethod(Func<List<int>, double> method)
        {
            _process = method;
        }

        public void SetPrint(Action<List<int>> print)
        {
            _printOut = print;
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
            _printOut(_values);
        }
        #endregion
    }
}
