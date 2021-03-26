namespace SportsStore.Contracts
{
    public class City
    {
        // Deze class bevat enkel POD types, dwz "simpele" classes als double, int, string, ...
        #region Properties
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
