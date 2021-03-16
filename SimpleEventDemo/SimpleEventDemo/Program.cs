using SimpleEventDemo.Events;
using System;

namespace SimpleEventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Counter(10);
            c.ThresholdReached += OnThresholdReached;
            c.CounterIncreased += OnCounterIncreased1;
            c.CounterIncreased += OnCounterIncreased1;
            c.CounterIncreased += OnCounterIncreased2;
            c.CounterIncreased += OnCounterIncreased3;
            c.CounterIncreased -= OnCounterIncreased1;
            c.CounterIncreased -= OnCounterIncreased1;
            c.CounterIncreased -= OnCounterIncreased1;
            c.ThresholdTimeReached += OnThresholdTimeReached;
            Console.WriteLine("Press 'a' key to increase total");
            while(Console.ReadKey(true).KeyChar == 'a')
            {
                c.Add(1);
            }
            System.Diagnostics.Debug.WriteLine("Total: " + c.Total);
        }

        private static void OnThresholdTimeReached(object sender, EventArgs e)
        {
            var args = e as ThresholdReachedEventArgs;
            System.Diagnostics.Debug.WriteLine("I'm finally here");
        }

        private static void OnCounterIncreased1(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            System.Diagnostics.Debug.WriteLine("Counter value (1): " + counter?.Total);
        }

        private static void OnCounterIncreased2(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            System.Diagnostics.Debug.WriteLine("Counter value (2): " + counter?.Total);
        }

        private static void OnCounterIncreased3(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            System.Diagnostics.Debug.WriteLine("Counter value (3): " + counter?.Total);
        }

        private static void OnCounterIncreased4(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            System.Diagnostics.Debug.WriteLine("Counter value (4): " + counter?.Total);
        }

        private static void OnThresholdReached(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            if(e == EventArgs.Empty)
            {
                System.Diagnostics.Debug.WriteLine("No args");
            }
            System.Diagnostics.Debug.WriteLine("Threshold exceeded: " + counter?.Total);
        }
    }
}
