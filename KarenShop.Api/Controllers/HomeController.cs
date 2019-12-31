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

        [HttpGet("{category}")]
        public async Task<ActionResult<ProductTypesDto>> GetProductTypesByCategory(string category)
        {
            try
            {
                return Ok(await _productRepository.GetProductTypesByCategory(await _productRepository.GetCategoryIdByEnName(category)));
            }
            catch (Exception)
            {
                return NotFound(new ProductTypesDto() { IsSuccess=false,Error="این آدرس وجود ندارد!!!"});
            }
        }
    }
}