using KarenShop.Api.Domains.Contracts;
using KarenShop.Api.Domains.Dtos;
using KarenShop.Api.Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarenShop.Api.Infrastructures.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        protected DbSet<Product> _products;
        protected DbSet<ProductType> _productTypes;
        protected DbSet<Category> _categories;
        protected DbSet<SubCategory> _subCategories;

        public ProductRepository(KarenShopDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Products;
            _productTypes = dbContext.ProductTypes;
            _categories = dbContext.Categories;
            _subCategories = dbContext.SubCategories;
        }

        public async Task<BaseResponseDto> AddPrice(NewPriceDto newPrice)
        {
            try
            {
                var product = await GetProduct(newPrice.ProductId);
                if (product.ProductPrices.Count(x => x.ShopUserId == newPrice.ShopUserId) > 0)
                {
                    product.ProductPrices.FirstOrDefault(x => x.ShopUserId == newPrice.ShopUserId).Price = newPrice.Price;
                    product.ProductPrices.FirstOrDefault(x => x.ShopUserId == newPrice.ShopUserId).Discount = newPrice.Discount;
                }
                else
                {
                    var price = new ProductPrice()
                    {
                        ShopUserId = newPrice.ShopUserId,
                        Price = newPrice.Price,
                        Discount = newPrice.Discount
                    };
                    product.ProductPrices.Add(price);
                }
                await _context.SaveChangesAsync();
                return new BaseResponseDto() { IsSuccess = true, Error = "" };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto() { IsSuccess = false, Error = "به دلایلی قادر به ذخیره سازی نیست لطفا پس از بررسی اطلاعات دوباره تلاش کنید..." };
            }
        }

        public async Task<int> GetCategoryIdByEnName(string enName)
        {
            return (await _categories.FirstOrDefaultAsync(x => x.EnName == enName)).Id;
        }

        public async Task<OffersDto> GetOffers()
        {
            var products = await _products.Where(x => x.ShowInOffer == true)
                .Select(x => new OfferDto()
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    OfferImage = x.PictureAddresses.FirstOrDefault(x => x.ShowInOffer == true).Uri,
                    Prices = new PricesDto()
                    {
                        Discount = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        OriginalPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price,
                        PayPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price -
                               x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        DiscountPercent = decimal.Parse((Math.Round((x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount /
                                        x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price) * 100, 0)).ToString("0")),
                        Description= x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Description
                    }
                }).ToListAsync();
            if (products.Count == 0)
            {
                return new OffersDto() { IsSuccess = false, Offers = null, Error = "کالایی برای پیشنهاد ویژه وجود ندارد!!!" };
            }
            return new OffersDto() { Offers = products, IsSuccess = true };
        }

        public async Task<ProductDetailDto> GetProductById(long product)
        {
            var productDetails = await _products.Where(x => x.Id == product).Select(x =>
                                new ProductDetailDto()
                                {
                                    Name = x.Name,
                                    Description = x.Description,
                                    Brand = "",
                                    Category = x.Category.Name,
                                    Colors = x.ProductColors.Select(y =>
                                        new ColorDto()
                                        {
                                            Id = y.Id,
                                            Name = y.ColorName,
                                            Value = y.ColorValue
                                        }).ToList(),
                                    Error = "",
                                    IsSuccess = true,
                                    MinPrice = new PricesDto()
                                    {
                                        Discount = x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Discount,
                                        OriginalPrice = x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Price,
                                        PayPrice = x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Price -
                                            x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Discount,
                                        DiscountPercent = decimal.Parse((Math.Round((x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Discount /
                                            x.ProductPrices.OrderBy(a => a.Price - a.Discount).FirstOrDefault().Price) * 100, 0)).ToString("0")),
                                        Description = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Description
                                    },
                                    Images = x.PictureAddresses.Select(x => x.Uri).ToList(),
                                    SubCategory = x.SubCategory.Name,
                                    ProductType = x.ProductType.Name,
                                    Resellers = x.ProductPrices.Select(y =>
                                          new ResellersDto()
                                          {
                                              Id = y.ShopUserId,
                                              ResellerName = string.IsNullOrWhiteSpace(y.ShopUser.CompanyName) ? y.ShopUser.FullName : y.ShopUser.CompanyName,
                                              Price = new PricesDto()
                                              {
                                                  Discount = y.Discount,
                                                  OriginalPrice = y.Price,
                                                  PayPrice = y.Price - y.Discount,
                                                  DiscountPercent = decimal.Parse((Math.Round((y.Discount /
                                                    y.Price) * 100, 0)).ToString("0")),
                                                  Description = y.Description
                                              }
                                          }).ToList(),
                                    Sizes = x.ProductSizes.Select(x => x.SizeName).ToList()
                                }).FirstOrDefaultAsync();
            if (productDetails == null)
                return new ProductDetailDto()
                {
                    IsSuccess = false,
                    Error = "چنین کالایی وجود ندارد"

                };
            return productDetails;
        }

        public async Task<ProductsDto> GetProductsByProductType(int productType)
        {
            var products = await _products.Where(x => x.ProductTypeId == productType).Select(
                x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.PictureAddresses.FirstOrDefault().Uri ?? "",
                    Prices = new PricesDto()
                    {
                        Discount = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        OriginalPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price,
                        PayPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price -
                               x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        DiscountPercent = decimal.Parse((Math.Round((x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount /
                                        x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price) * 100, 0)).ToString("0")),
                        Description = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Description
                    }
                }).ToListAsync();
            var type = await _productTypes.Include(x => x.SubCategory).ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.ProductTypeId == productType);
            if (products.Count == 0)
                return new ProductsDto()
                {
                    IsSuccess = false,
                    Error = "محصولی برای این دسته بندی ثبت نشده است!!!",
                    products = null,
                    Brands = null,
                    MaxPriceFilter = 0,
                    MinPriceFilter = 0,
                    ProductType = type.Name,
                    SubCategory = type.SubCategory?.Name,
                    Category = type.SubCategory?.Category?.Name
                };
            else
                return new ProductsDto()
                {
                    IsSuccess = true,
                    Error = "",
                    products = products,
                    Brands = null,
                    MaxPriceFilter = products.Max(x => x.Prices.OriginalPrice),
                    MinPriceFilter = products.Min(x => x.Prices.PayPrice),
                    ProductType = type.Name,
                    SubCategory = type.SubCategory?.Name,
                    Category = type.SubCategory?.Category?.Name
                };
        }

        public async Task<ProductsDto> GetProductsBySubCategory(int subCategory)
        {
            var products = await _products.Where(x => x.SubCategoryId == subCategory).Select(
                x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.PictureAddresses.FirstOrDefault().Uri ?? "",
                    Prices = new PricesDto()
                    {
                        Discount = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        OriginalPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price,
                        PayPrice = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price -
                               x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount,
                        DiscountPercent = decimal.Parse((Math.Round((x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Discount /
                                        x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Price) * 100, 0)).ToString("0")),
                        Description = x.ProductPrices.OrderBy(x => x.Price - x.Discount).FirstOrDefault().Description
                    }
                }).ToListAsync();
            var type = await _productTypes.Include(x => x.SubCategory).ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.SubCategoryId == subCategory);
            if (products.Count == 0)
                return new ProductsDto()
                {
                    IsSuccess = false,
                    Error = "محصولی برای این دسته بندی ثبت نشده است!!!",
                    products = null,
                    Brands = null,
                    MaxPriceFilter = 0,
                    MinPriceFilter = 0,
                    ProductType = type.Name,
                    SubCategory = type.SubCategory?.Name,
                    Category = type.SubCategory?.Category?.Name
                };
            else
                return new ProductsDto()
                {
                    IsSuccess = true,
                    Error = "",
                    products = products,
                    Brands = null,
                    MaxPriceFilter = products.Max(x => x.Prices.OriginalPrice),
                    MinPriceFilter = products.Min(x => x.Prices.PayPrice),
                    ProductType = type.Name,
                    SubCategory = type.SubCategory?.Name,
                    Category = type.SubCategory?.Category?.Name
                };
        }

        public async Task<ProductTypesDto> GetProductTypesByCategory(int catId, int? subCatId)
        {
            var types = await _productTypes.Where(x => x.SubCategory.CategoryId == catId && (x.SubCategoryId == subCatId || subCatId == null))
                        .Select(x =>
                            new ProductTypeDto()
                            {
                                Name = x.Name,
                                ProductTypeId = x.ProductTypeId,
                                ProductTypeImage = x.ProductTypePictureUri
                            }
                        ).ToListAsync();
            if (types.Count == 0)
                return new ProductTypesDto()
                {
                    IsSuccess = false,
                    Error = "نوع محصولی برای این کتگوری ثبت نشده است!!!",
                    ProductTypes = null
                };
            return new ProductTypesDto()
            {
                IsSuccess = true,
                ProductTypes = types
            };
        }

        public async Task<Product> GetProduct(long product)
        {
            return await _products.Include(x => x.ProductPrices)
                .FirstOrDefaultAsync(x => x.Id == product);
        }

        public async Task<int?> GetSubCategoryIdByEnName(string enName)
        {
            if (string.IsNullOrWhiteSpace(enName))
                return null;
            return (await _subCategories.FirstOrDefaultAsync(x => x.EnName == enName)).Id;
        }
    }
}
