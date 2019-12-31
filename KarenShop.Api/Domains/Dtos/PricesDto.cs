using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Dtos
{
    public class PricesDto
    {
        public decimal OriginalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal PayPrice { get; set; }
    }
}
