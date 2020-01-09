namespace KarenShop.Api.Domains.Models
{
    public class ProductPrice
    {
        public long Id { get; set; }
        public int ShopUserId { get; set; }
        public ShopUser ShopUser { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}