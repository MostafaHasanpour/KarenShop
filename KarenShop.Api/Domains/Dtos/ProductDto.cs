namespace KarenShop.Api.Domains.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PricesDto Prices { get; set; }
        public string Picture { get; set; }
    }
}