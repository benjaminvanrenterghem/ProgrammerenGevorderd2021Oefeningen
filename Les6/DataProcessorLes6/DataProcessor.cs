using System.Collections.Generic;
//using System.Linq;
using System;

namespace ProcessorExample
{
    public class DataProcessor
    {
        #region Events
        public delegate double ProcessMethod(List<int> values);
        #endregion

        #region Properties
        private /*ProcessMethod*/Func<List<int>, double> _process;
        private List<int> _values = new List<int>();
        private double _result;
        private Action<List<int>> _printOut;
        #endregion

        #region Ctor
        public DataProcessor(List<int> values)
        {
            foreach (var i in values)
                _values.Add(i);
        }

        public void SetPrint(Action<List<int>> print)
        {
            _printOut = print;
        }

        public void PrintValues()
        {
            _printOut(_values);
        }
        #endregion

        #region Methods
        public void SetMethod(/*ProcessMethod*/Func<List<int>, double> method)
        {
            _process = method;
        }

        public void ProcessValues()
        {
            _result = _process(_values);
        }

        public void PrintResult()
        {
            System.Diagnostics.Debug.WriteLine("Result: {0}", _result);
        }
        #endregion
    }
}
