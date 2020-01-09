using KarenShop.Api.Domains.Dtos;
using KarenShop.Api.Domains.Models;
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
        Task<int?> GetSubCategoryIdByEnName(string enName);
        Task<ProductTypesDto> GetProductTypesByCategory(int catId,int? subCatId);
        Task<ProductsDto> GetProductsByProductType(int productType);
        Task<ProductsDto> GetProductsBySubCategory(int subCategory);
        Task<ProductDetailDto> GetProductById(long product);
        Task<BaseResponseDto> AddPrice(NewPriceDto newPrice);
        Task<Product> GetProduct(long productId);
    }
}
