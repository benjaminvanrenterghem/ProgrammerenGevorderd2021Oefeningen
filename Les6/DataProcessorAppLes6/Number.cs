namespace DataProcessorAppLes6
{
    public class Number
    {
        private PrintHelper _printHelper = new PrintHelper();
        public int Value { get; set; }

        public Number(int val)
        {
            Value = val;
            //_printHelper.BeforePrintEvent += _printHelperBeforePrintEvent; // Ik abonneer mij op de event (let op de bliksem!) Deze method kan je automatisch aanmaken met de TAB toets!
        }

        private void _printHelperBeforePrintEvent(string message)
        {
            System.Diagnostics.Debug.WriteLine("BeforePrintEvent fires from " + message);
        }

        public void PrintMoney()
        {
            _printHelper.PrintMoney(Value);
        }

        public void PrintNumber()
        {
            _printHelper.PrintNumber(Value);
        }

    }
}
