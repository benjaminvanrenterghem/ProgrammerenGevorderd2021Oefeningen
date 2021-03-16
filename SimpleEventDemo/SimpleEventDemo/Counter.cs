using SimpleEventDemo.Events;
using System;

namespace SimpleEventDemo
{
    public class Counter
    {
        #region Properties
        private int _total = 0;
        public int Total { get { return _total; } } // read-only, 0 by default
        private int _threshold = 0;
        #endregion

        #region Events
        // event die ik aanbied
        public event EventHandler ThresholdReached;
        public event EventHandler ThresholdTimeReached;
        public event EventHandler CounterIncreased;
        #endregion

        #region Ctor
        public Counter(int threshold)
        {
            _threshold = threshold;
        }
        #endregion

        #region Methods
        public void Add(int step)
        {
            _total += step;
            CounterIncreased?.Invoke(this, EventArgs.Empty);
            if(_total >= _threshold)
            {
                // ik moet wie geabonneerd is op een event, waarschuwen dwz oproepen
                // ? operator die test of ThresholdReached != 0
                // alternatief voor if(ThresholdReached != null) ThresholdReached(this, EventArgs.Empty);
                ThresholdReached?.Invoke(this, EventArgs.Empty); // event zonder argument geeft toch altijd Empty door
                ThresholdTimeReached?.Invoke(this, new ThresholdReachedEventArgs { Threshold = _threshold, TimeReached = DateTime.Now });
            }
        }
        #endregion
    }
}
