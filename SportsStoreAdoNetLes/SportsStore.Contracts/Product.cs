namespace SportsStore.Contracts
{
    public class Product
    {
        #region Properties // hart van de class: de identiteit van een object wordt bepaald door de concrete data
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } // decimal: liever decimal dan double voor bedragen; decimal wordt afgekapt en is geen benadering van de prijs
        #endregion
    }
}
