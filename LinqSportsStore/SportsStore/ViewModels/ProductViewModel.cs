namespace SportsStore.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEuro { get { return Price * 0.9M; } }
    }
}
