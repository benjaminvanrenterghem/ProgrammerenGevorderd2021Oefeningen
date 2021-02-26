namespace WinkelManagement
{
    public class Bestelling
    {
        #region Properties
        public ProductType ProductType { get; set; }
        public double Prijs { get; set; }
        public int Aantal { get; set; }
        public string Adres { get; set; }
        #endregion

        #region Ctor
        public Bestelling(ProductType product, int aantal)
        {
            ProductType = product;
            Aantal = aantal;
        }

        public Bestelling(ProductType product, double prijs, int aantal, string adres)
        {
            ProductType = product;
            Prijs = prijs;
            Aantal = aantal;
            Adres = adres;
        }
        #endregion
    }
}
