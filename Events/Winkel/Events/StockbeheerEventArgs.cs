namespace WinkelManagement.Events
{
    public class StockbeheerEventArgs
    {
        #region Properties
        public ProductType Type { get; set; }
        public int Aantal { get; set; }
        #endregion

        #region Ctor
        public StockbeheerEventArgs(ProductType type, int aantal)
        {
            Type = type;
            Aantal = aantal;
        }
        #endregion
    }
}
