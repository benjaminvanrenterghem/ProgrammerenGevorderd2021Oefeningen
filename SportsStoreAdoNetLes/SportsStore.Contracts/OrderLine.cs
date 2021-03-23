namespace SportsStore.Contracts
{
    public class OrderLine
    {
        #region Properties
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        #endregion
    }
}
