using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarenShop.Api.Domains.Contracts;
using KarenShop.Api.Domains.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarenShop.Api.Controllers
{
    [Route("api/v1/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("offers")]
        public async Task<OffersDto> GetOffers()
        {
            return await _productRepository.GetOffers();
        }

        [HttpGet("{category}/{subCategory?}")]
        public async Task<ActionResult<ProductTypesDto>> GetProductTypesByCategory(string category, string subCategory = "")
        {
            try
            {
                return Ok(await _productRepository.GetProductTypesByCategory(await _productRepository.GetCategoryIdByEnName(category), await _productRepository.GetSubCategoryIdByEnName(subCategory)));
            }
            catch (Exception)
            {
                return NotFound(new ProductTypesDto() { IsSuccess = false, Error = "این آدرس وجود ندارد!!!" });
            }
        }
    }
}