namespace WinkelManagement.Events
{
    public class WinkelEventArgs
    {
        #region Properties
        public Bestelling Bestelling { get; set; }
        #endregion

        #region Ctor
        public WinkelEventArgs(Bestelling b)
        {
            Bestelling = b;
        }
        #endregion
    }
}
