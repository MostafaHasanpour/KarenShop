namespace KarenShop.Api.Domains.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
