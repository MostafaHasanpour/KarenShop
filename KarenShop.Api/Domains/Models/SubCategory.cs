namespace KarenShop.Api.Domains.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
