using System.Collections.Generic;
//using System.Linq;

namespace ProcessorExample
{
    public class DataProcessor
    {
        #region Events
        public delegate double ProcessMethod(List<int> values);
        #endregion

        #region Properties
        private ProcessMethod _process;
        private List<int> _values = new List<int>();
        private double _result;
        #endregion

        #region Ctor
        public DataProcessor(List<int> values)
        {
            foreach (var i in values)
                _values.Add(i);
        }

        public void AddMethod(ProcessMethod method)
        {
            _process += method;
        }
        #endregion

        #region Methods
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
            System.Diagnostics.Debug.WriteLine("Result: {0}", _result);
        }
        #endregion
    }
}
