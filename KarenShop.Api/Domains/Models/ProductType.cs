namespace KarenShop.Api.Domains.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public string ProductTypePictureUri { get; set; }
    }
}
