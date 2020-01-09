using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarenShop.Api.Domains.Contracts;
using KarenShop.Api.Domains.Dtos;
using KarenShop.Api.Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarenShop.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{productType}")]
        public async Task<ProductsDto> Get(int? productType)
        {
            try
            {
                return await _productRepository.GetProductsByProductType(productType ?? 0);
            }
            catch (Exception)
            {
                return new ProductsDto()
                {
                    IsSuccess = false,
                    Error = "این دسته بندی پیدا نشد!!!",
                    products = null,
                    Brands = null,
                    MaxPriceFilter = 0,
                    MinPriceFilter = 0,
                };
            }

        }

        //**************************
        [HttpGet("sub-category/{subCategory}")]
        public async Task<ProductsDto> GetBySubCategory(int? SubCategory)
        {
            try
            {
                return await _productRepository.GetProductsBySubCategory(SubCategory ?? 0);
            }
            catch (Exception)
            {
                return new ProductsDto()
                {
                    IsSuccess = false,
                    Error = "این دسته بندی پیدا نشد!!!",
                    products = null,
                    Brands = null,
                    MaxPriceFilter = 0,
                    MinPriceFilter = 0,
                };
            }

        }
        //****************************
        [HttpGet("detail/{product}")]
        public async Task<ProductDetailDto> Details(long? product)
        {
            try
            {
                return await _productRepository.GetProductById(product ?? 0);
            }
            catch (Exception)
            {
                return new ProductDetailDto()
                {
                    IsSuccess = false,
                    Error = "این کالا پیدا نشد!!!",
                };
            }
        }

        [HttpPost("add-price")]
        public async Task<BaseResponseDto> AddPrice(NewPriceDto newPrice)
        {
            return await _productRepository.AddPrice(newPrice);
        }
    }
}