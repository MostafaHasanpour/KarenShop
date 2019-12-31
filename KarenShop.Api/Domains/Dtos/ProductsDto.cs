using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Dtos
{
    public class ProductsDto : BaseResponseDto
    {
        public string ProductType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public List<string> Brands { get; set; }
        public decimal MinPriceFilter { get; set; }
        public decimal MaxPriceFilter { get; set; }
        public List<ProductDto> products { get; set; }
    }
}
