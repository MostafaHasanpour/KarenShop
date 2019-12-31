using KarenShop.Api.Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Domains.Contracts
{
    public interface IProductRepository
    {
        Task<OffersDto> GetOffers();
        Task<int> GetCategoryIdByEnName(string enName);
        Task<ProductTypesDto> GetProductTypesByCategory(int catId);
        Task<ProductsDto> GetProductsByProductType(int productType);
        Task<ProductsDto> GetProductsBySubCategory(int subCategory);
        Task<ProductDetailDto> GetProductById(long product);
    }
}
