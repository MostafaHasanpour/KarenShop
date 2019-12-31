using System.Collections.Generic;

namespace KarenShop.Api.Domains.Dtos
{
    public class ProductDetailDto : BaseResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public List<string> Images { get; set; }
        public List<string> Sizes { get; set; }
        public List<ColorDto> Colors { get; set; }
        public PricesDto MinPrice { get; set; }
        public List<ResellersDto> Resellers { get; set; }
    }

    public class ColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ResellersDto
    {
        public int Id { get; set; }
        public PricesDto Price { get; set; }
        public string ResellerName { get; set; }
    }
}
