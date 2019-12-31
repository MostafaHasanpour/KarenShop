namespace KarenShop.Api.Domains.Models
{
    public class ProductSize
    {
        public long Id { get; set; }
        public string SizeName { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

    }
}