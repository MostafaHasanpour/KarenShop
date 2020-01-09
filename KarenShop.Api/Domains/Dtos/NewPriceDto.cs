namespace KarenShop.Api.Domains.Dtos
{
    public class NewPriceDto
    {
        public long ProductId { get; set; }
        public int ShopUserId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
    }
}
