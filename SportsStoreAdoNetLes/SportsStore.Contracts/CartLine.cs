namespace SportsStore.Contracts
{

    public class CartLine
    {
        #region Properties  
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        #endregion
    }
}
