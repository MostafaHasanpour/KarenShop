using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool ShowInOffer { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public virtual List<ProductPrice> ProductPrices { get; set; }
        public virtual List<ProductSize> ProductSizes { get; set; }
        public virtual List<ProductColor> ProductColors { get; set; }
        public virtual List<ProductPictureAddress> PictureAddresses { get; set; }
    }
}
