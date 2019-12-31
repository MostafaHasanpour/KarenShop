using System.Collections.Generic;

namespace KarenShop.Api.Domains.Dtos
{
    public class ProductTypeDto
    {
        public string ProductTypeImage { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
    }
    public class ProductTypesDto:BaseResponseDto
    {
        public List<ProductTypeDto> ProductTypes { get; set; }
    }
}
